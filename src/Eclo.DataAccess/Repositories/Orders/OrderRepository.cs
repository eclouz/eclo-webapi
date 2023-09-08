using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Orders;
using Eclo.DataAccess.ViewModels.Orders;
using Eclo.Domain.Entities.Orders;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Repositories.Orders;

public class OrderRepository : BaseRepository, IOrderRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM orders";
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

    public async Task<int> CreateAsync(Order entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.orders " +
                "(user_id, products_price, status, description, is_contracted, is_paid, payment_type, created_at, updated_at) " +
                "VALUES (@UserId, @ProductsPrice, @Status, @Description, @IsContracted, @IsPaid, @PaymentType, @CreatedAt, @UpdatedAt); ";

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
            string query = "DELETE FROM orders WHERE id = @Id";
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

            string query = $"SELECT * FROM orders ORDER BY id DESC " +
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
            string query = "SELECT * FROM orders WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<OrderViewModel>(query, new { Id = id });

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

    public async Task<int> UpdateAsync(long id, Order entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"UPDATE public.orders " +
                $"SET user_id=@UserId, products_price=@ProductsPrice, status=@Status, description=@Description, " +
                    $"is_contracted=@IsContracted, is_paid=@IsPaid, payment_type=@PaymentType, " +
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