using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Payments;
using Eclo.DataAccess.ViewModels.Orders;
using Eclo.Domain.Entities.Payments;

namespace Eclo.DataAccess.Repositories.Payments;

public class PaymentRepository : BaseRepository, IPaymentRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM payments";
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

    public async Task<int> CreateAsync(Payment entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.payments " +
                "(order_detail_id, transaction_status, description, created_at, updated_at) " +
                "VALUES (@OrderDetailId, @TransactionStatus, @Description, @CreatedAt, @UpdatedAt); ";

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
            string query = "DELETE FROM payments WHERE id = @Id";
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

    public async Task<IList<Payment>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM payments ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

            var result = (await _connection.QueryAsync<Payment>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<Payment>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Payment?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM payments WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<Payment>(query, new { Id = id });

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

    public async Task<(long ItemsCount, IList<Payment>)> SearchAsync(string search, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM payments WHERE transaction_status ILIKE @search ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

            var result = (await _connection.QueryAsync<Payment>
                (query, new { search = "%" + search + "%" })).ToList();

            return (result.Count, result);
        }
        catch
        {
            return (0, new List<Payment>());
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Payment entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"UPDATE public.payments SET " +
                $"order_detail_id=@OrderDetailId, transaction_status=@TransactionStatus, " +
                    $"description=@Description, created_at=@CreatedAt, updated_at=@UpdatedAt " +
                        $"WHERE id = @Id; ";


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