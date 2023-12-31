﻿using Eclo.Application.Utilities;
using Eclo.Domain.Entities.Products;
using Eclo.Persistence.Dtos.Products;

namespace Eclo.Services.Interfaces.Products;

public interface IProductCommentService
{
    public Task<bool> CreateAsync(ProductCommentCreateDto dto);

    public Task<bool> DeleteAsync(long productCommentId);

    public Task<long> CountAsync();

    public Task<IList<ProductComment>> GetAllAsync(PaginationParams @params);

    public Task<ProductComment> GetByIdAsync(long productCommentId);

    public Task<bool> UpdateAsync(long productCommentId, ProductCommentUpdateDto dto);
}
