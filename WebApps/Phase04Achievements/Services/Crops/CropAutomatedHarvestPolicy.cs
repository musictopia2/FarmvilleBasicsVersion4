namespace Phase04Achievements.Services.Crops;
public class CropAutomatedHarvestPolicy : ICropHarvestPolicy
{
    Task<bool> ICropHarvestPolicy.IsAutomaticAsync()
    {
        return Task.FromResult(true);
    }
}