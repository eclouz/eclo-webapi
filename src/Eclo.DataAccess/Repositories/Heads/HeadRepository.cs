using Dapper;
using Eclo.DataAccess.Interfaces.Heads;
using Eclo.Domain.Entities.Heads;
using static Dapper.SqlMapper;

namespace Eclo.DataAccess.Repositories.Heads;

public class HeadRepository : BaseRepository, IHeadRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select Count(*) From heads";
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

    public async Task<int> CreateAsync(Head entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.heads (first_name, last_name, phone_number, phone_number_confirmed, " +
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
            string query = $"DELETE FROM heads WHERE id=@Id";
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

    public async Task<Head?> GetById(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * From heads where id=@Id";
            var result = await _connection.QuerySingleAsync<Head>(query, new { Id = id });

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

    public async Task<Head?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM heads WHERE id = {id};";
            var result = await _connection.QuerySingleAsync<Head>(query);

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

    public async Task<Head?> GetByPhoneAsync(string phone)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM heads WHERE phone_number = @PhoneNumber";
            var data = await _connection.QuerySingleAsync<Head>(query, new { PhoneNumber = phone });
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

    public async Task<int> UpdateAsync(long id, Head entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.heads SET " +
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

    public async Task<int> UpdatePhoneNumberAsync(string phoneNumber, Head entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "UPDATE public.heads SET " +
                "first_name=@FirstName, last_name=@LastName, image_path=@ImagePath, passport_serial_number=@PassportSerialNumber, " +
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
