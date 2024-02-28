using FlowerShop.ApplicationService.DataTransferObjects.Flower;
using FlowerShop.ApplicationService.Interfaces.Services;
using FlowerShop.Business.Settings.NotificationSettings;
using FlowerShop.Business.Settings.PaginationSettings;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class FlowerController(IFlowerService flowerService) : ControllerBase
{
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> AddAsync([FromBody] FlowerSave flowerSave) =>
        flowerService.AddAsync(flowerSave);

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> UpdateAsync([FromBody] FlowerUpdate flowerUpdate) =>
       flowerService.UpdateAsync(flowerUpdate);

    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> DeleteAsync([FromQuery] int id) =>
       flowerService.DeleteAsync(id);

    [HttpGet("get-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlowerResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<FlowerResponse?> GetByIdAsync([FromQuery] int id) =>
       flowerService.GetByIdAsync(id);

    [HttpGet("get-all-paginated")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageList<FlowerResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<PageList<FlowerResponse>> GetAllPaginatedAsync([FromQuery] PageParameters pageParameters) =>
       flowerService.GetAllPaginatedAsync(pageParameters);
}
