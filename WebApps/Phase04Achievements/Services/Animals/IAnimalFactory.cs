using Phase04Achievements.Services.Core;

namespace Phase04Achievements.Services.Animals;
public interface IAnimalFactory
{
    AnimalServicesContext GetAnimalServices(FarmKey farm);
}