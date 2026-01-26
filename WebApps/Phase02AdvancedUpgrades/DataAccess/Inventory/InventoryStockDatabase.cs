namespace Phase02AdvancedUpgrades.DataAccess.Inventory;
public class InventoryStockDatabase() : ListDataAccess<InventoryStockDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration, IInventoryRepository, IInventoryStarterRepository

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "InventoryStock";
    async Task<Dictionary<string, int>> IInventoryStarterRepository.GetBaseLineAsync(FarmKey farm)
    {
        var list = await GetDocumentsAsync();
        return list.GetSingleDocument(farm).Baseline;
    }
    async Task<Dictionary<string, int>> IInventoryRepository.LoadAsync(FarmKey farm)
    {
        var list = await GetDocumentsAsync();
        return list.Single(x => x.Farm.Equals(farm)).Current;
    }

    async Task IInventoryRepository.SaveAsync(FarmKey farm, Dictionary<string, int> items)
    {
        var list = await GetDocumentsAsync();

        var current = list.Single(x => x.Farm.Equals(farm));
        current.Current = items;
        await UpsertRecordsAsync(list);
    }

}