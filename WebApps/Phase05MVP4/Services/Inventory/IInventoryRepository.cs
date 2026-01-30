namespace Phase05MVP4.Services.Inventory;
public interface IInventoryRepository
{
    Task SaveAsync(FarmKey farm, Dictionary<string, int> items);
    Task<Dictionary<string, int>> LoadAsync(FarmKey farm);
}