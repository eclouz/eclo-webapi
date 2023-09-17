using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Interfaces.Products;

public interface IProductCommentRepository : IRepository<ProductComment, ProductComment>,
    IGetAll<ProductComment>
{
    public Task<ProductComment?> GetById(long id);
}
