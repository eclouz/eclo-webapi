using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.DataAccess.ViewModels.Products;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Repositories.Products;

public class ProductRepository : BaseRepository, IProductRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM products";
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

    public async Task<int> CreateAsync(Product entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.products " +
                "(brand_id, sub_category_id, name, unit_price, description, created_at, updated_at) " +
                    "VALUES (@BrandId, @SubCategoryId, @Name, @UnitPrice, @Description, @CreatedAt, @UpdatedAt);";

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
            string query = "DELETE FROM products WHERE id = @Id";
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

    public async Task<IList<Product>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM products ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

            var result = (await _connection.QueryAsync<Product>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<Product>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<IList<ProductGetViewModel>> GetAllView(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> GetById(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM products WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<Product>(query, new { Id = id });

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

    public async Task<Product?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM products WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<Product>(query, new { Id = id });

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

    public async Task<(long ItemsCount, IList<Product>)> SearchAsync(string search, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM products WHERE product_name ILIKE @search ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

            var result = (await _connection.QueryAsync<Product>
                (query, new { search = "%" + search + "%" })).ToList();

            return (result.Count, result);
        }
        catch
        {
            return (0, new List<Product>());
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Product entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"UPDATE public.products " +
                $"SET brand_id=@BrandId, sub_category_id=@SubCategoryId, name=@Name, unit_price=@UnitPrice, " +
                    $"description=@Description, created_at=@CreatedAt, updated_at=@UpdatedAt " +
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