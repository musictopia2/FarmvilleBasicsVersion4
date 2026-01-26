namespace Phase02AdvancedUpgrades.Services.Animals;
public interface IAnimalRegistry
{
    Task<BasicList<AnimalRecipe>> GetAnimalsAsync();
}