﻿using Eclo.Application.Exceptions.Admins;
using Eclo.Application.Exceptions.Auth;
using Eclo.DataAccess.Interfaces.Admins;
using Eclo.DataAccess.Interfaces.Heads;
using Eclo.Persistence.Dtos.Auth;
using Eclo.Persistence.Dtos.Notifications;
using Eclo.Persistence.Dtos.Security;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Auth;
using Eclo.Services.Interfaces.Notifications;
using Eclo.Services.Security;
using Microsoft.Extensions.Caching.Memory;

namespace Eclo.Services.Services.Auth;

public class AdminAuthService : IAdminAuthService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IHeadRepository _headRepository;
    private readonly ITokenService _tokenService;
    private readonly IMemoryCache _memoryCache;
    private readonly ISmsSender _smsSender;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;
    private const string VERIFY_RESET_CACHE_KEY = "verify_reset_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";

    public AdminAuthService(IAdminRepository adminRepository,
        IHeadRepository headRepository,
        ITokenService tokenService,
        IMemoryCache memoryCache,
        ISmsSender smsSender)
    {
        this._adminRepository = adminRepository;
        this._headRepository = headRepository;
        this._tokenService = tokenService;
        this._memoryCache = memoryCache;
        this._smsSender = smsSender;
    }

#pragma warning disable

    public async Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
    {
        var head = await _headRepository.GetByPhoneAsync(loginDto.PhoneNumber);
        if (head == null)
        {
            var admin = await _adminRepository.GetByPhoneNumberAsync(loginDto.PhoneNumber);
            if (admin == null) throw new AdminNotFoundException();

            var hasherResult = PasswordHasher.Verify(loginDto.Password, admin.PasswordHash, admin.Salt);
            if (hasherResult == false) throw new PasswordNotMatchException();

            string token = await _tokenService.GenerateToken(admin);
            return (Result: true, Token: token);
        }
        else
        {
            var hasherResult = PasswordHasher.Verify(loginDto.Password, head.PasswordHash, head.Salt);
            if (hasherResult == false) throw new PasswordNotMatchException();

            string token = await _tokenService.GenerateToken(head);
            return (Result: true, Token: token);
        }
    }

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForResetPasswordAsync(string phone)
    {
        var admin = await _adminRepository.GetByPhoneNumberAsync(phone);
        if (admin is null) throw new AdminNotFoundException();
        VerificationDto verificationDto = new VerificationDto();
        verificationDto.Attempt = 0;
        verificationDto.CreatedAt = TimeHelper.GetDateTime();
        verificationDto.Code = CodeGenerator.GenerateRandomNumber();

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


        var smsResult = await _smsSender.SendAsync(smsMessage);
        if (smsResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
        else return (Result: false, CachedVerificationMinutes: 0);
    }

    public async Task<(bool Result, string Token)> VerifyResetPasswordAsync(string phone, int code)
    {
        if (_memoryCache.TryGetValue(VERIFY_RESET_CACHE_KEY + phone, out VerificationDto verificationDto))
        {
            if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                throw new VerificationTooManyRequestsException();
            else if (verificationDto.Code == code)
            {
                var user = await _adminRepository.GetByPhoneNumberAsync(phone);
                string token = _tokenService.GenerateToken(user).ToString();

                return (Result: true, Token: token);
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
}
