namespace Phase02AdvancedUpgrades.Services.Items;
public interface IItemPlanProvider
{
    Task<BasicList<ItemPlanModel>> GetPlanAsync(FarmKey farm);
}