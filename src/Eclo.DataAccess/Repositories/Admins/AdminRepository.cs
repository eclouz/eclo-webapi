using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Admins;
using Eclo.Domain.Entities.Admins;

namespace Eclo.DataAccess.Repositories.Admins;

public class AdminRepository : BaseRepository, IAdminRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select Count(*) From admins";
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

    public async Task<int> CreateAsync(Admin entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.admins(first_name, last_name, phone_number, phone_number_confirmed, " +
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
            string query = "DELETE FROM admins WHERE id=@Id;";
            var result = await _connection.ExecuteAsync(query, new { Id = id });

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

    public async Task<IList<Admin>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "SELECT * FROM public.admins Order By id Desc " +
                $"Offset {@params.GetSkipCount()} Limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<Admin>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<Admin>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<Admin>> GetAllAsync()
    {
        try
        {
            await _connection.OpenAsync();

            string query = "SELECT * FROM public.admins Order By id Desc ";

            var result = (await _connection.QueryAsync<Admin>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<Admin>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Admin?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * From admins where id=@Id";
            var result = await _connection.QuerySingleAsync<Admin>(query, new { Id = id });

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

    public async Task<Admin?> GetByPhoneNumberAsync(string phoneNumber)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM admins WHERE phone_number = @PhoneNumber";
            var data = await _connection.QuerySingleAsync<Admin>(query, new { PhoneNumber = phoneNumber });

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

    public async Task<(long ItemsCount, IList<Admin>)> SearchAsync(string search, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"Select * From admins Where first_name ILIKE @search Order By first_name " +
                $"Offset {@params.GetSkipCount()} Limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<Admin>(query, new { search = "%" + search + "%" })).ToList();
            long count = result.LongCount();

            return (count, result);
        }
        catch
        {
            return (0, new List<Admin>());
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Admin entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "UPDATE public.admins SET " +
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
}
