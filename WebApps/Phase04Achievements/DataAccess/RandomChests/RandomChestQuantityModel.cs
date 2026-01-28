namespace Phase04Achievements.DataAccess.RandomChests;
public class RandomChestQuantityModel
{
    public string TargetName { get; set; } = ""; //examples are coin, etc.
    public int MinimumQuantity { get; set; }
    public int MaximumQuantity { get; set; }
}