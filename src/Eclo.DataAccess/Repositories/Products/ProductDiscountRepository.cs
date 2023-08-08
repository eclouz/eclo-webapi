using Dapper;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.Domain.Entities.Discounts;

namespace Eclo.DataAccess.Repositories.Products;

public class ProductDiscountRepository : BaseRepository, IProductDiscountRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM product_discounts";
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

    public async Task<int> CreateAsync(ProductDiscount entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.product_discounts " +
                "(product_id, discount_id, description, start_at, end_at, created_at, updated_at) " +
                "VALUES (@ProductId, @DiscountId, @Description, @StartAt, @EndAt, @CreatedAt, @UpdatedAt);";

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
            string query = "DELETE FROM product_discounts WHERE id = #Id";
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

    public async Task<ProductDiscount?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM product_discounts WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<ProductDiscount>(query, new { Id = id });

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

    public async Task<int> UpdateAsync(long id, ProductDiscount entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"UPDATE public.product_discounts " +
                $"SET product_id=@ProductId, discount_id=@DiscountId, description=@Description, " +
                $"start_at=@StartAt, end_at=@EndAt, created_at=CreatedAt, updated_at=@UpdatedAt " +
                $"WHERE id = @Id;";

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