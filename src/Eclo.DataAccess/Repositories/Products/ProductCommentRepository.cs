using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Repositories.Products;

public class ProductCommentRepository : BaseRepository, IProductCommentRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM product_comments";
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

    public async Task<int> CreateAsync(ProductComment entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.product_comments(user_id, product_id, " +
                    "comment, is_edited, created_at, updated_at) " +
                        "VALUES (@UserId, @ProductId, @Comment, @IsEdited, " +
                            "@CreatedAt, @UpdatedAt);";

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
            string query = "DELETE FROM product_comments WHERE id=@Id";
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

    public async Task<IList<ProductComment>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM product_comments ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

            var result = (await _connection.QueryAsync<ProductComment>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<ProductComment>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<ProductComment?> GetById(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM product_comments WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<ProductComment>(query, new { Id = id });

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

    public async Task<ProductComment?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM product_comments WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<ProductComment>(query, new { Id = id });

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

    public async Task<int> UpdateAsync(long id, ProductComment entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"UPDATE public.product_comments " +
                $"SET product_id=@ProductId, user_id=@UserId, reply_comment_id=@ReplyCommentId, comment=@Comment, " +
                    $"is_edited=@IsEdited, created_at=@CreatedAt, updated_at=@UpdatedAt " +
                        $"WHERE id=@Id;";

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
