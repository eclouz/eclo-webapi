using Dapper;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Payments;
using Eclo.Domain.Entities.Payments;

namespace Eclo.DataAccess.Repositories.Payments;

public class CardRepository : BaseRepository, ICardRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM user_cards";
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

    public async Task<int> CreateAsync(Card entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.user_cards " +
                "(user_id, card_holder_name, card_number, balance, pin_code, expired_month, expired_year, is_active, created_at, " +
                    "updated_at) VALUES (@UserId, @CardHolderName, @CardNumber, @Balance, @PinCode, @ExpiredMonth, @ExpiredYear, @IsActive, @CreatedAt, @UpdatedAt); ";

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
            string query = "DELETE FROM user_cards WHERE id = @Id";
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

    public async Task<IList<Card>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM user_cards ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

            var result = (await _connection.QueryAsync<Card>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<Card>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Card?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM user_cards WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<Card>(query, new { Id = id });

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

    public async Task<(long ItemsCount, IList<Card>)> SearchAsync(string search, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM user_cards WHERE card_holder_name ILIKE @search ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

            var result = (await _connection.QueryAsync<Card>
                (query, new { search = "%" + search + "%" })).ToList();

            return (result.Count, result);
        }
        catch
        {
            return (0, new List<Card>());
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Card entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"UPDATE public.user_cards " +
                $"SET user_id=@UserId, card_holder_name=@CardHolderName, card_number=@CardNumber, balance=@Balance, " +
                    $"pin_code=@PinCode, expired_month=@ExpiredMonth, expired_year=@ExpiredYear, is_active=@IsActive, " +
                        $"created_at=@CreatedAt, updated_at=@UpdatedAt WHERE id = @Id; ";

            //string query = $"UPDATE public.user_transactions " +
            //    $"SET user_id=@UserId, sender_card_number=@SenderCardNumber, receiver_card_number=@ReceiverCardNumber, " +
            //        $"required_amount=@RequiredAmount, is_transfered=@IsTransfered, status=@Status, " +
            //            $"created_at=@CreatedAt, updated_at=@UpdatedAt WHERE id = @Id; ";

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
