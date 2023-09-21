using Eclo.Domain.Entities.Heads;

namespace Eclo.DataAccess.Interfaces.Heads;

public interface IHeadRepository : IRepository<Head, Head>
{
    public Task<Head?> GetByPhoneAsync(string phone);
    public Task<Head?> GetById(long id);

    public Task<int> UpdatePhoneNumberAsync(string phoneNumber, Head dto);
}
