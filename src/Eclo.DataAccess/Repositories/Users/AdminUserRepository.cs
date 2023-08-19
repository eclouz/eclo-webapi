using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Users;
using Eclo.DataAccess.ViewModels.Users;
using Eclo.Domain.Entities.Users;

namespace Eclo.DataAccess.Repositories.Users;

public class AdminUserRepository : BaseRepository, IAdminUserRepository
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

    public Task<int> CreateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "DELETE FROM public.users WHERE id=@Id;";
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

    public async Task<IList<AdminUserViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "SELECT * FROM public.admin_user_view Order By id Desc " +
                $"Offset {@params.GetSkipCount()} Limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<AdminUserViewModel>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<AdminUserViewModel>();
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

    public async Task<AdminUserViewModel?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * From admin_user_view where id=@Id";
            var result = await _connection.QuerySingleAsync<AdminUserViewModel>(query, new { Id = id });

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

    public async Task<(long ItemsCount, IList<AdminUserViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"Select * From admin_user_view Where first_name ILIKE @search Order By first_name " +
                $"Offset {@params.GetSkipCount()} Limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<AdminUserViewModel>(query, new { search = "%" + search + "%" })).ToList();
            long count = result.LongCount();

            return (count, result);
        }
        catch
        {
            return (0, new List<AdminUserViewModel>());
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<int> UpdateAsync(long id, User entity)
    {
        throw new NotImplementedException();
    }
}
