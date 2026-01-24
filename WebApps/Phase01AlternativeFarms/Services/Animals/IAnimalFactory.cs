using Phase01AlternativeFarms.Services.Core;

namespace Phase01AlternativeFarms.Services.Animals;
public interface IAnimalFactory
{
    AnimalServicesContext GetAnimalServices(FarmKey farm);
}