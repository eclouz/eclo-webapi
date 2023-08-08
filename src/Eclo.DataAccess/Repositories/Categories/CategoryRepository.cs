using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Categories;
using Eclo.Domain.Entities.Categories;

namespace Eclo.DataAccess.Repositories.Categories;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM categories";
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

    public Task<int> CreateAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Category>> GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(long id, Category entity)
    {
        throw new NotImplementedException();
    }
}
