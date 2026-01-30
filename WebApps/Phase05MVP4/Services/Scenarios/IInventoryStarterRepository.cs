namespace Phase05MVP4.Services.Scenarios;
public interface IInventoryStarterRepository
{
    Task<Dictionary<string, int>> GetBaseLineAsync(FarmKey farm);
}