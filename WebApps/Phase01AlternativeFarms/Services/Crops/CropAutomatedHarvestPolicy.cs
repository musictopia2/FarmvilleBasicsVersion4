namespace Phase01AlternativeFarms.Services.Crops;
public class CropAutomatedHarvestPolicy : ICropHarvestPolicy
{
    Task<bool> ICropHarvestPolicy.IsAutomaticAsync()
    {
        return Task.FromResult(true);
    }
}