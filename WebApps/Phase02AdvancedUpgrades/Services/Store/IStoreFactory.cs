namespace Phase02AdvancedUpgrades.Services.Store;
public interface IStoreFactory
{
    StoreServicesContext GetStoreServices(FarmKey farm);
}