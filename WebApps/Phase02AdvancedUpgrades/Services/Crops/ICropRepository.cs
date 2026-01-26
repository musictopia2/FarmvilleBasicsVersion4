namespace Phase02AdvancedUpgrades.Services.Crops;
public interface ICropRepository
{
    Task<CropSystemState> LoadAsync();
    Task SaveAsync(CropSystemState state);
}