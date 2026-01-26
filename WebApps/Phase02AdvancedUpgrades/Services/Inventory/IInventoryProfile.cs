namespace Phase02AdvancedUpgrades.Services.Inventory;
public interface IInventoryProfile
{
    Task<InventoryStorageProfileModel> LoadAsync();
    Task SaveAsync(InventoryStorageProfileModel profile);
}