namespace Phase02AdvancedUpgrades.DataAccess.Crops;
public class CropRecipeDocument
{
    required public string Item { get; init; }
    required public TimeSpan Duration { get; init; }
    required public string Theme { get; init; }
    required public BasicList<int> TierLevelRequired { get; init; } = [];
    required public bool IsFast { get; init; }
}