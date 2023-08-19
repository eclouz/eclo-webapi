using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.Domain.Entities.Products;
using static Dapper.SqlMapper;

namespace Eclo.DataAccess.Repositories.Products;

public class UserProductLikeRepository : BaseRepository, IUserProductLikeRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM user_product_likes";
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

    public async Task<int> CreateAsync(UserProductLike entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.user_product_likes " +
                "(user_id, product_id, is_liked, created_at, updated_at) " +
                    "VALUES (@UserId, @ProductId, @IsLiked, @CreatedAt, @UpdatedAt);";

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
            string query = "DELETE FROM user_product_likes WHERE id = @Id";
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

    public async Task<IList<UserProductLike>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM user_product_likes ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize};";

            var result = (await _connection.QueryAsync<UserProductLike>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<UserProductLike>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<UserProductLike?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM user_product_likes WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<UserProductLike>(query, new { Id = id });

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

    public async Task<int> UpdateAsync(long id, UserProductLike entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"UPDATE public.user_product_likes " +
                $"SET user_id=@UserId, product_id=@ProductId, is_liked=@IsLiked, " +
                    $"created_at=@CreatedAt, updated_at=@UpdatedAt" +
                        $"WHERE id = {id};";

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