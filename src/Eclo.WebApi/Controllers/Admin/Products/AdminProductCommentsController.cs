using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Admin.Products;

[Route("api/admin/product/comments")]
[ApiController]
public class AdminProductCommentsController : AdminBaseController
{
    private readonly IProductCommentService _service;

    public AdminProductCommentsController(IProductCommentService service)
    {
        this._service = service;
    }

    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteAsync(long commentId)
        => Ok(await _service.DeleteAsync(commentId));
}
