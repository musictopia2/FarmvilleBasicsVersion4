namespace Phase02AdvancedUpgrades.Services.Progression;
public interface IAnimalProgressionPlanProvider
{
    Task<BasicList<ItemUnlockRule>> GetPlanAsync(FarmKey farm);
}