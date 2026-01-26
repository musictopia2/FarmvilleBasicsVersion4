namespace Phase02AdvancedUpgrades.Services.Items;
public interface IItemFactory
{
    ItemServicesContext GetItemServices(FarmKey farm);
}