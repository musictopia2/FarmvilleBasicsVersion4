namespace Phase05MVP4.DataAccess.RandomChests;
public class RandomChestItemWeightModel
{
    public string Category { get; set; } = "";
    public string TargetName { get; set; } = ""; //needs this for image.
    public int ItemWeight { get; set; }
    public int LeveRequired { get; set; } //you may be required to be a certain level first.
    public TimeSpan? ReducedBy { get; set; }
    public TimeSpan? Duration { get; set; }
    public string? OutputAugmentationKey { get; set; }
}