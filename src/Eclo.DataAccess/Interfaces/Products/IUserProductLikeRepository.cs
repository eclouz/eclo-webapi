﻿using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Interfaces.Products;

public interface IUserProductLikeRepository : IRepository<UserProductLike, UserProductLike>,
    IGetAll<UserProductLike>
{
}
