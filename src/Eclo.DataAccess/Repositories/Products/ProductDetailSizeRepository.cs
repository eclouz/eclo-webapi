using Dapper;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.Domain.Entities.Discounts;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Repositories.Products;

public class ProductDetailSizeRepository : BaseRepository, IProductDetailSizeRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM product_detail_sizes";
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

    public async Task<int> CreateAsync(ProductDetailSize entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.product_detail_sizes (product_detail_id, size, quantity, created_at, updated_at) " +
                "VALUES (@ProductDetailId, @Size, @Quantity, @CreatedAt, @UpdatedAt);";
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
            string query = "DELETE FROM product_detail_sizes WHERE id = #Id";
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

    public async Task<ProductDetailSize?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM product_detail_sizes WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<ProductDetailSize>(query, new { Id = id });
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

    public async Task<int> UpdateAsync(long id, ProductDetailSize entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE public.product_detail_sizes " +
                $"SET product_detail_id=@ProductDetailId, size=@Size, quantity=@Quantity, created_at=@CreatedAt, updated_at=@UpdatedAt " +
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