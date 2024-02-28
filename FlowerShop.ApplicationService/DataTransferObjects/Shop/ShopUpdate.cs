namespace FlowerShop.ApplicationService.DataTransferObjects.Shop;
public sealed record ShopUpdate(int Id,
                                string Name,
                                string Location,
                                string Email,
                                List<int> FlowerIds);
