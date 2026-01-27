namespace Phase03RandomChests.Services.Crops;
public class CropRecipe
{
    public string Item { get; init; } = "";
    public TimeSpan Duration { get; init; }
    required public BasicList<int> TierLevelRequired { get; init; } = [];
    required public bool IsFast { get; init; }
}