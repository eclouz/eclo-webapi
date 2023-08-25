using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Users;
using Eclo.DataAccess.ViewModels.Users;
using Eclo.Domain.Entities.Users;

namespace Eclo.DataAccess.Repositories.Users;

public class UserRepository : BaseRepository, IUserRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select Count(*) From users";
            var result = await _connection.QuerySingleAsync<long>(query);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync(); 
        }
    }

    public async Task<int> CreateAsync(User entity)
    {
        try
        {
            await _connection.OpenAsync();
            
            string query = "INSERT INTO public.users(first_name, last_name, phone_number, phone_number_confirmed, " +
                "password_hash, salt, image_path, passport_serial_number, " +
                    "birth_date, region, district, address, created_at, updated_at) " +
                "VALUES (@FirstName, @LastName, @PhoneNumber, @PhoneNumberConfirmed, " +
                    "@PasswordHash, @Salt, @ImagePath, @PassportSerialNumber, @BirthDate, " +
                        "@Region, @District, @Address, @CreatedAt, @UpdatedAt);";

            var result = await _connection.ExecuteAsync(query, entity);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "DELETE FROM public.users WHERE id=@Id;";
            var result=await _connection.ExecuteAsync(query, new { Id=id });

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "SELECT * FROM public.user_view Order By id Desc " +
                $"Offset {@params.GetSkipCount()} Limit {@params.PageSize}";
            
            var result = (await _connection.QueryAsync<UserViewModel>(query)).ToList();
            
            return result;
        }
        catch
        {
            return new List<UserViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<User?> GetById(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * From users where id=@Id";
            var result = await _connection.QuerySingleAsync<User>(query, new { Id = id });

            return result;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<UserViewModel?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * From user_view where id=@Id";
            var result = await _connection.QuerySingleAsync<UserViewModel>(query, new { Id = id });
           
            return result;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<User?> GetByPhoneAsync(string phone)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM users WHERE phone_number = @PhoneNumber";
            var data = await _connection.QuerySingleAsync<User>(query, new { PhoneNumber = phone });
            return data;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<(long ItemsCount, IList<UserViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
         
            string query = $"Select * From user_view Where first_name ILIKE @search Order By first_name " +
                $"Offset {@params.GetSkipCount()} Limit {@params.PageSize}";
            
            var result = (await _connection.QueryAsync<UserViewModel>(query, new { search = "%" + search + "%" })).ToList();
            long count = result.LongCount();
            
            return (count, result);
        }
        catch
        {
            return (0, new List<UserViewModel>());
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, User entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "UPDATE public.users SET " +
                "first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, phone_number_confirmed=@PhoneNumberConfirmed, " +
                    "password_hash=@PasswordHash, salt=@Salt, image_path=@ImagePath, passport_serial_number=@PassportSerialNumber, " +
                        "birth_date=@BirthDate, region=@Region, district=@District, address=@Address, created_at=@CreatedAt, updated_at=@UpdatedAt " +
                $"WHERE id={id};";
            
            var result = await _connection.ExecuteAsync(query, entity);
            
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdatePhoneNumberAsync(string phoneNumber, User entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "UPDATE public.users SET " +
                "first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, phone_number_confirmed=@PhoneNumberConfirmed, " +
                    "password_hash=@PasswordHash, salt=@Salt, image_path=@ImagePath, passport_serial_number=@PassportSerialNumber, " +
                        "birth_date=@BirthDate, region=@Region, district=@District, address=@Address, created_at=@CreatedAt, updated_at=@UpdatedAt " +
                $"WHERE phone_number=@phoneNumber;";

            var result = await _connection.ExecuteAsync(query, entity);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
