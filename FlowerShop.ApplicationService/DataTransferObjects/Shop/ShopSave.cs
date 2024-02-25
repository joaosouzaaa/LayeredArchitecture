namespace FlowerShop.ApplicationService.DataTransferObjects.Shop;
public sealed record ShopSave(string Name,
                              string Location,
                              string Email,
                              List<int> FlowerIds);
