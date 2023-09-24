using Eclo.Application.Exceptions.Auth;
using Eclo.Application.Exceptions.Users;
using Eclo.DataAccess.Interfaces.Users;
using Eclo.Domain.Entities.Users;
using Eclo.Persistence.Dtos.Auth;
using Eclo.Persistence.Dtos.Notifications;
using Eclo.Persistence.Dtos.Security;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Auth;
using Eclo.Services.Interfaces.Notifications;
using Eclo.Services.Security;
using Microsoft.Extensions.Caching.Memory;

namespace Eclo.Services.Services.Auth;

public class UserAuthService : IUserAuthService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IUserRepository _userRepository;
    private readonly ISmsSender _smsSender;
    private readonly ITokenService _tokenService;
    private const int CACHED_MINUTES_FOR_REGISTER = 60;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;
    private const string REGISTER_CACHE_KEY = "register_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
    private const string VERIFY_RESET_CACHE_KEY = "verify_reset_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;

    public UserAuthService(IMemoryCache memoryCache,
        IUserRepository userRepository,
        ISmsSender smsSender,
        ITokenService tokenService)
    {
        this._memoryCache = memoryCache;
        this._userRepository = userRepository;
        this._smsSender = smsSender;
        this._tokenService = tokenService;
    }

#pragma warning disable

    public async Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetByPhoneAsync(loginDto.PhoneNumber);
        if (user is null) throw new UserNotFoundException();

        var hasherResult = PasswordHasher.Verify(loginDto.Password, user.PasswordHash, user.Salt);
        if (hasherResult == false) throw new PasswordNotMatchException();

        string token = await _tokenService.GenerateToken(user);
        return (Result: true, Token: token);
    }

    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto)
    {
        var user = await _userRepository.GetByPhoneAsync(dto.PhoneNumber);
        if (user is not null) throw new UserAlreadyExistsException(dto.PhoneNumber);

        // delete if exists user by this phone number
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.PhoneNumber, out RegisterDto cachedRegisterDto))
        {
            cachedRegisterDto.FirstName = cachedRegisterDto.FirstName;
            _memoryCache.Remove(REGISTER_CACHE_KEY + dto.PhoneNumber);
        }
        else _memoryCache.Set(REGISTER_CACHE_KEY + dto.PhoneNumber, dto,
            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));

        return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER);
    }

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterDto registerDto))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();

            // make confirm code as random
            //verificationDto.Code = CodeGenerator.GenerateRandomNumber();
            verificationDto.Code = 11111;


            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto oldVerifcationDto))
            {
                _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
            }

            _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

            SmsMessage smsMessage = new SmsMessage();
            smsMessage.Title = "Eclo";
            smsMessage.Content = "Your verification code : " + verificationDto.Code;
            smsMessage.Recipent = phone.Substring(1);

            //var smsResult = await _smsSender.SendAsync(smsMessage);
            var smsResult = true;

            if (smsResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
            else return (Result: false, CachedVerificationMinutes: 0);
        }
        else throw new UserCacheDataExpiredException();
    }

    public async Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterDto registerDto))
        {
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto verificationDto))
            {
                if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                    throw new VerificationTooManyRequestsException();
                else if (verificationDto.Code == code)
                {
                    var dbResult = await RegisterToDatabaseAsync(registerDto);
                    if (dbResult is true)
                    {
                        var user = await _userRepository.GetByPhoneAsync(phone);
                        string token = await _tokenService.GenerateToken(user);
                        return (Result: true, Token: token);
                    }
                    else return (Result: false, Token: "");
                }
                else
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
                    verificationDto.Attempt++;
                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                        TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));
                    return (Result: false, Token: "");
                }
            }
            else throw new VerificationCodeExpiredException();
        }
        else throw new UserCacheDataExpiredException();
    }

    private async Task<bool> RegisterToDatabaseAsync(RegisterDto registerDto)
    {
        var userCheck = await _userRepository.GetByPhoneAsync(registerDto.PhoneNumber);
        if (userCheck is not null) throw new UserAlreadyExistsException();
        else
        {
            var user = new User();
            user.FirstName = registerDto.FirstName;
            user.LastName = registerDto.LastName;
            user.PhoneNumber = registerDto.PhoneNumber;
            user.PhoneNumberConfirmed = true;

            var hasherResult = PasswordHasher.Hash(registerDto.Password);
            user.PasswordHash = hasherResult.Hash;
            user.Salt = hasherResult.Salt;

            user.CreatedAt = user.UpdatedAt = TimeHelper.GetDateTime();
            user.ImagePath = "Avatars\\avatar.png";

            var dbResult = await _userRepository.CreateAsync(user);
            return dbResult > 0;
        }
    }

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForResetPasswordAsync(string phone)
    {
        var user = await _userRepository.GetByPhoneAsync(phone);
        if (user is null) throw new UserNotFoundException();
        VerificationDto verificationDto = new VerificationDto();
        verificationDto.Attempt = 0;
        verificationDto.CreatedAt = TimeHelper.GetDateTime();
        //verificationDto.Code = CodeGenerator.GenerateRandomNumber();
        verificationDto.Code = 11111;

        if (_memoryCache.TryGetValue(VERIFY_RESET_CACHE_KEY + phone, out VerificationDto oldVerifcationDto))
        {
            _memoryCache.Remove(VERIFY_RESET_CACHE_KEY + phone);
        }

        _memoryCache.Set(VERIFY_RESET_CACHE_KEY + phone, verificationDto,
            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

        SmsMessage smsMessage = new SmsMessage();
        smsMessage.Title = "Eclo";
        smsMessage.Content = "Your verification code : " + verificationDto.Code;
        smsMessage.Recipent = phone.Substring(1);

        //var smsResult = await _smsSender.SendAsync(smsMessage);
        var smsResult = true;
        if (smsResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
        else return (Result: false, CachedVerificationMinutes: 0);
    }

    public async Task<(bool Result, string Token)> VerifyResetPasswordAsync(string phone, int code)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out ResetPasswordDto resetDto))
        {
            if (_memoryCache.TryGetValue(VERIFY_RESET_CACHE_KEY + phone, out VerificationDto verificationDto))
            {
                if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                    throw new VerificationTooManyRequestsException();
                else if (verificationDto.Code == code)
                {
                    var dbResult = await ResetPasswordAsync(resetDto);
                    if (dbResult is true)
                    {
                        var user = await _userRepository.GetByPhoneAsync(phone);
                        string token = await _tokenService.GenerateToken(user);
                        return (Result: true, Token: token);
                    }
                    else return (Result: false, Token: "");

                }
                else
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
                    verificationDto.Attempt++;
                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                        TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

                    return (Result: false, Token: "");
                }
            }
            else throw new VerificationCodeExpiredException();
        }
        else throw new UserCacheDataExpiredException();
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
    {
        var user = await _userRepository.GetByPhoneAsync(dto.PhoneNumber);
        if (user is null) throw new UserNotFoundException();
        var hasherResult = PasswordHasher.Hash(dto.Password);
        user.PasswordHash = hasherResult.Hash;

        user.Salt = hasherResult.Salt;
        user.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _userRepository.UpdateAsync(user.Id, user);

        return result > 0;
    }

    public async Task<(bool Result, int CachedMinutes)> UpdatePasswordAsync(ResetPasswordDto dto)
    {
        var user = await _userRepository.GetByPhoneAsync(dto.PhoneNumber);
        if (user is null) throw new UserNotFoundException();

        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.PhoneNumber, out ResetPasswordDto cachedResetPasswordDto))
        {
            _memoryCache.Remove(REGISTER_CACHE_KEY + dto.PhoneNumber);
        }
        else _memoryCache.Set(REGISTER_CACHE_KEY + dto.PhoneNumber, dto,
            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));

        return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER);
    }
}
