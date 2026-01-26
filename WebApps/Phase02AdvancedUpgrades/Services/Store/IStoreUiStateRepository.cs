namespace Phase02AdvancedUpgrades.Services.Store;
public interface IStoreUiStateRepository
{
    Task<EnumCatalogCategory> LoadAsync();
    Task SaveAsync(EnumCatalogCategory category);
}