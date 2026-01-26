namespace Phase02AdvancedUpgrades.Services.Animals;
public interface IAnimalCollectionPolicy
{
    Task<EnumAnimalCollectionMode> GetCollectionModeAsync();
}