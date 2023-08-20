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

    [HttpDelete("{productCommentId}")]
    public async Task<IActionResult> DeleteAsync(long productCommentId)
        => Ok(await _service.DeleteAsync(productCommentId));
}
