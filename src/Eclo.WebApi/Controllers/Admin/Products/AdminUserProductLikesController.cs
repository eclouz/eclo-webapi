using Eclo.Persistence.Dtos.Products;
using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Admin.Products;

[Route("api/admin/user/product/likes")]
[ApiController]
public class AdminUserProductLikesController : ControllerBase
{
    private readonly IUserProductLikeService _service;

    public AdminUserProductLikesController(IUserProductLikeService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] UserProductLikeCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }

    [HttpPut("{likeId}")]
    public async Task<IActionResult> UpdateAsync(long likeId, [FromForm] UserProductLikeUpdateDto dto)
    {
        return Ok(await _service.UpdateAsync(likeId, dto));
    }

    [HttpDelete("{likeId}")]
    public async Task<IActionResult> DeleteAsync(long likeId)
       => Ok(await _service.DeleteAsync(likeId));
}
