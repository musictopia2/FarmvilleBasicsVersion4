namespace Phase02AdvancedUpgrades.Services.Scenarios;
public interface IInventoryStarterRepository
{
    Task<Dictionary<string, int>> GetBaseLineAsync(FarmKey farm);
}