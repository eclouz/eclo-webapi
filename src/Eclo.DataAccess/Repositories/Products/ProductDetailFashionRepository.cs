using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Repositories.Products;

public class ProductDetailFashionRepository : BaseRepository, IProductDetailFashionRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from product_detail_fashions";
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

    public async Task<int> CreateAsync(ProductDetailFashion entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.product_detail_fashions(product_detail_id, image_path, created_at, " +
                "updated_at) VALUES (@ProductDetailId,@ImagePath,@CreatedAt,@UpdatedAt);";

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
            string query = "DELETE FROM public.product_detail_fashions WHERE id=@Id;";
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

    public async Task<IList<ProductDetailFashion>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM public.product_detail_fashions order by id desc " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<ProductDetailFashion>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<ProductDetailFashion>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<ProductDetailFashion?> GetById(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM public.product_detail_fashions where id=@Id";
            var result = await _connection.QuerySingleAsync<ProductDetailFashion>(query, new { Id = id });

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

    public async Task<ProductDetailFashion?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM public.product_detail_fashions where id=@Id";
            var result = await _connection.QuerySingleAsync<ProductDetailFashion>(query, new { Id = id });

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

    public async Task<int> UpdateAsync(long id, ProductDetailFashion entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "UPDATE public.product_detail_fashions " +
                "SET product_detail_id=@ProductDetailId, image_path=@ImagePath, updated_at=@UpdatedAt " +
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
