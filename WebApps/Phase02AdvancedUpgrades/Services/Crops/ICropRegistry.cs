namespace Phase02AdvancedUpgrades.Services.Crops;
public interface ICropRegistry
{
    Task<BasicList<CropRecipe>> GetCropsAsync();

}