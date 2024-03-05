using FlowerShop.ApplicationService.DataTransferObjects.Shop;
using FlowerShop.ApplicationService.Interfaces.Services;
using FlowerShop.CrossCutting.Settings.NotificationSettings;
using FlowerShop.CrossCutting.Settings.PaginationSettings;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class ShopController(IShopService shopService) : ControllerBase
{
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> AddAsync([FromBody] ShopSave shopSave) =>
        shopService.AddAsync(shopSave);

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> UpdateAsync([FromBody] ShopUpdate shopUpdate) =>
       shopService.UpdateAsync(shopUpdate);

    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> DeleteAsync([FromQuery] int id) =>
       shopService.DeleteAsync(id);

    [HttpGet("get-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShopResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<ShopResponse?> GetByIdAsync([FromQuery] int id) =>
       shopService.GetByIdAsync(id);

    [HttpGet("get-all-paginated")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageList<ShopResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<PageList<ShopResponse>> GetAllPaginatedAsync([FromQuery] PageParameters pageParameters) =>
       shopService.GetAllPaginatedAsync(pageParameters);
}
