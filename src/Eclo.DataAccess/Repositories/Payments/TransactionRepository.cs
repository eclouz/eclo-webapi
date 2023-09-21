using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Payments;
using Eclo.Domain.Entities.Payments;

namespace Eclo.DataAccess.Repositories.Payments;

public class TransactionRepository : BaseRepository, ITransactionRepository
{
    public Task<long> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreateAsync(Transaction entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.user_transactions " +
                "(user_id, sender_card_number, receiver_card_number, required_amount, is_transfered, status, created_at, updated_at) " +
                    "VALUES (@UserId, @SenderCardNumber, @ReceiverCardNumber, @RequiredAmount, @IsTransfered, @Status, @CreatedAt, @UpdatedAt); ";

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

    public Task<int> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Transaction>> GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> TransactionProcedure(string senderCardNumber, string receiverCardNumber, double requiredAmount)
    {
        try
        {
            await _connection.OpenAsync();
            var p = new DynamicParameters();
            p.Add("sender_card_number", senderCardNumber);
            p.Add("receiver_card_number", receiverCardNumber);
            p.Add("amount", requiredAmount);
            var result = await _connection.QueryAsync<int>("transfer", p, commandType: System.Data.CommandType.StoredProcedure);
            return 1;
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

    public Task<int> UpdateAsync(long id, Transaction entity)
    {
        throw new NotImplementedException();
    }
}
