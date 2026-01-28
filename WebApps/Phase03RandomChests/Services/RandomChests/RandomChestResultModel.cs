namespace Phase03RandomChests.Services.RandomChests;
public class RandomChestResultModel
{
    public TimeSpan? ReduceBy { get; init; }
    public int Quantity { get; init; } = 1;
    public TimeSpan? Duration { get; set; }
    public string? TargetName { get; init; }
    public string? Source { get; init; }
    public string? OutputAugmentationKey { get; init; }
}