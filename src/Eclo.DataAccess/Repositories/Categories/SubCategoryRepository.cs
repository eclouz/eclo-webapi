using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Categories;
using Eclo.Domain.Entities.Categories;

namespace Eclo.DataAccess.Repositories.Categories;

public class SubCategoryRepository : BaseRepository, ISubCategoryRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM sub_categories";
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

    public async Task<int> CreateAsync(SubCategory entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.sub_categories(category_id, name, created_at, updated_at) " +
                "VALUES (@CategoryId, @Name, @CreatedAt, @UpdatedAt);";

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
            string query = "DELETE FROM sub_categories WHERE id=@Id";
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

    public async Task<IList<SubCategory>> GetAllAsync(long id, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM sub_categories ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize};";

            var result = (await _connection.QueryAsync<SubCategory>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<SubCategory>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<SubCategory?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM sub_categories WHERE id=@Id";
            var result = await _connection.QuerySingleAsync<SubCategory>(query, new { Id = id });

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

    public async Task<int> UpdateAsync(long id, SubCategory entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"UPDATE public.sub_categories " +
                $"SET category_id=@CategoryId, name=@Name, created_at=@CreatedAt, updated_at=@UpdatedAt " +
                    $"WHERE id={id}";

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
