using Phase03RandomChests.Services.Core;

namespace Phase03RandomChests.Services.Animals;
public interface IAnimalFactory
{
    AnimalServicesContext GetAnimalServices(FarmKey farm);
}