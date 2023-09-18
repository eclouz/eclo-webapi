using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.DataAccess.ViewModels.Products;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Repositories.Products
{
    public class ProductDetailRepository : BaseRepository, IProductDetailRepository
    {
        public async Task<long> CountAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"select count(*) from product_details";
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

        public async Task<int> CreateAsync(ProductDetail entity)
        {
            try
            {
                await _connection.OpenAsync();

                string query = "INSERT INTO public.product_details(product_id, image_path, color, created_at," +
                    "updated_at) VALUES (@ProductId,@ImagePath,@Color,@CreatedAt,@UpdatedAt);";

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
                string query = "DELETE FROM product_details WHERE id=@Id";
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

        public async Task<IList<ProductDetail>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"SELECT * FROM product_details order by id desc " +
                    $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

                var result = (await _connection.QueryAsync<ProductDetail>(query)).ToList();

                return result;
            }
            catch
            {
                return new List<ProductDetail>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<IList<ProductDetailViewModel>> GetAllProductDetailsAsync(long productId)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"SELECT * FROM product_detail_view where product_id=@ProductId";

                var result = (await _connection.QueryAsync<ProductDetailViewModel>(query, new { ProductId = productId })).ToList();

                return result;
            }
            catch
            {
                return new List<ProductDetailViewModel>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<ProductDetail?> GetById(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM product_details where id=@Id";
                var result = await _connection.QuerySingleAsync<ProductDetail>(query, new { Id = id });

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

        public async Task<ProductDetail?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM product_details where id=@Id";
                var result = await _connection.QuerySingleAsync<ProductDetail>(query, new { Id = id });

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

        public async Task<ProductDetailViewModel?> GetByProductIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM product_detail_view where product_id=@Id";
                var result = await _connection.QuerySingleAsync<ProductDetailViewModel>(query, new { ProductId = id });

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

        public async Task<(long ItemsCount, IList<ProductDetail>)> SearchAsync(string search, PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"select * from product_details where product_name ilike '%{search}%'";
                var result = (await _connection.QueryAsync<ProductDetail>(query)).ToList();

                return (result.Count, result);
            }
            catch
            {
                return (0, new List<ProductDetail>());
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<int> UpdateAsync(long id, ProductDetail entity)
        {
            try
            {
                await _connection.OpenAsync();

                string query = "UPDATE public.product_details SET product_id=@ProductId, image_path=@ImagePath, " +
                    $"color=@Color, updated_at=@UpdatedAt WHERE id={id};";

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
}
