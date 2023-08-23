using Eclo.Application.Exceptions.Products;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.DataAccess.ViewModels.Products;
using Eclo.Domain.Entities.Products;
using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Products;

namespace Eclo.Services.Services.Products;

public class ProductCommentService : IProductCommentService
{
    private readonly IProductCommentRepository _repository;
    private readonly IPaginator _paginator;

    public ProductCommentService(IProductCommentRepository productCommentRepository,
        IPaginator paginator)
    {
        this._repository = productCommentRepository;
        this._paginator = paginator;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> CreateAsync(ProductCommentCreateDto dto)
    {
        ProductComment productComment = new ProductComment()
        {
            ProductId = dto.ProductId,
            UserId = dto.UserId,
            ReplyCommentId = dto.ReplyCommentId,
            Comment = dto.Comment,
            IsEdited = dto.IsEdited,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _repository.CreateAsync(productComment);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(long productCommentId)
    {
        var productComment = await _repository.GetById(productCommentId);
        if (productComment == null) throw new ProductCommentNotFoundException();

        var dbResult = await _repository.DeleteAsync(productCommentId);

        return dbResult > 0;
    }

    public async Task<IList<ProductComment>> GetAllAsync(PaginationParams @params)
    {
        var pproductComments = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return pproductComments;
    }

    public async Task<ProductComment> GetByIdAsync(long productCommentId)
    {
        var productComment = await _repository.GetByIdAsync(productCommentId);
        if (productComment == null) throw new ProductCommentNotFoundException();
        else return productComment;
    }

    public async Task<bool> UpdateAsync(long productCommentId, ProductCommentUpdateDto dto)
    {
        var productComment = await _repository.GetById(productCommentId);
        if (productComment == null) throw new ProductCommentNotFoundException();

        // update product with new items 
        productComment.ProductId = dto.ProductId;
        productComment.UserId = dto.UserId;
        productComment.ReplyCommentId = dto.ReplyCommentId;
        productComment.Comment = dto.Comment;
        productComment.IsEdited = dto.IsEdited;
        productComment.CreatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(productCommentId, productComment);

        return dbResult > 0;
    }
}
