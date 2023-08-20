using Eclo.Persistence.Dtos.Products;
using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.User.Products;

[Route("api/user/product/comments")]
[ApiController]
public class UserProductCommentsController : UserBaseController
{
    private readonly IProductCommentService _service;

    public UserProductCommentsController(IProductCommentService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] ProductCommentCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }
}
