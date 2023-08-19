using Eclo.DataAccess.Common;
using Eclo.DataAccess.ViewModels.Products;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Interfaces.Products;

public interface IProductCommentRepository : IRepository<ProductComment, ProductCommentViewModel>,
    IGetAll<ProductCommentViewModel>
{
    public Task<ProductComment?> GetById(long id);
}
