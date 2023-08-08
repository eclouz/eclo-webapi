using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.DataAccess.ViewModels.Products;
using Eclo.Domain.Entities.Categories;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Repositories
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

        public async Task<IList<ProductViewModel>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                
                string query = $"SELECT * FROM product_details order by id desc " +
                    $"offset {@params.GetSkipCount()} limit {@params.PageSize}";
                    
                var result = (await _connection.QueryAsync<ProductViewModel>(query)).ToList();

                return result;
            }
            catch
            {
                return new List<ProductViewModel>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<ProductViewModel?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM product_details where id=@Id";
                var result = await _connection.QuerySingleAsync<ProductViewModel>(query, new { Id = id });

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

        public async Task<(long ItemsCount, IList<ProductViewModel>)> SearchAsync(string search, PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"select * from product_view where product_name ilike '%{search}%'";
                var result = (await _connection.QueryAsync<ProductViewModel>(query)).ToList();

                return (result.Count, result);
            }
            catch 
            {
                return (0,new List<ProductViewModel>());                
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
