using Phase05MVP4.Services.Core;

namespace Phase05MVP4.Services.Animals;
public interface IAnimalFactory
{
    AnimalServicesContext GetAnimalServices(FarmKey farm);
}