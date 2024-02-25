using FlowerShop.ApplicationService.DataTransferObjects.Enums;

namespace FlowerShop.ApplicationService.DataTransferObjects.Flower;
public sealed record FlowerUpdate(int Id,
                                  string Name,
                                  string Color,
                                  string Species,
                                  EBloomingSeasonRequest BloomingSeason);
