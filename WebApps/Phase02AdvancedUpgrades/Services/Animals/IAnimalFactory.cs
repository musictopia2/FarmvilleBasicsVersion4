using Phase02AdvancedUpgrades.Services.Core;

namespace Phase02AdvancedUpgrades.Services.Animals;
public interface IAnimalFactory
{
    AnimalServicesContext GetAnimalServices(FarmKey farm);
}