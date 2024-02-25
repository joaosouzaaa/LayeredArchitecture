using FlowerShop.ApplicationService.DataTransferObjects.Enums;

namespace FlowerShop.ApplicationService.DataTransferObjects.Flower;
public sealed record FlowerSave(string Name,
                                string Color,
                                string Species,
                                EBloomingSeasonRequest BloomingSeason);
