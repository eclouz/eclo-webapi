using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Orders;
using Eclo.DataAccess.ViewModels.Orders;
using Eclo.Domain.Entities.Orders;

namespace Eclo.DataAccess.Repositories.Orders;

public class OrderDetailRepository : BaseRepository, IOrderDetailRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM order_details";
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

    public async Task<long> CountOrderViewAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM order_view";
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

    public async Task<int> CreateAsync(OrderDetail entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.order_details " +
                "(order_id, product_discount_id, quantity, price, discount_price, total_price, created_at, updated_at) " +
                    "VALUES (@OrderId, @ProductDiscountId, @Quantity, @Price, @DiscountPrice, " +
                        "@TotalPrice, @CreatedAt, @UpdatedAt); ";

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
            string query = "DELETE FROM order_details WHERE id = @Id";
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

    public async Task<IList<OrderViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            
            string query = $"SELECT * FROM order_view ORDER BY order_id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

            var result = (await _connection.QueryAsync<OrderViewModel>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<OrderViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<OrderViewModel?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM order_view WHERE order_id = @OrderId";
            var result = await _connection.QuerySingleAsync<OrderViewModel>(query, new { OrderId = id });

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

    public async Task<(long ItemsCount, IList<OrderViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM order_view WHERE product_name ILIKE @search ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

            var result = (await _connection.QueryAsync<OrderViewModel>
                (query, new { search = "%" + search + "%" })).ToList();

            return (result.Count, result);
        }
        catch
        {
            return (0, new List<OrderViewModel>());
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, OrderDetail entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"UPDATE public.order_details " +
                $"SET order_id=@OrderId, product_discount_id=@ProductDiscountId, quantity=@Quantity, price=@Price, " +
                    $"discount_price=@DiscountPrice, total_price=@TotalPrice, " +
                        $"created_at=@CreatedAt, updated_at=@UpdatedAt " +
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