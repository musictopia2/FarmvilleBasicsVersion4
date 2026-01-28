namespace Phase20RandomChests.Models;
public class RandomChestQuantityModel
{
    public string TargetName { get; set; } = ""; //examples are coin, etc.
    public int MinimumQuantity { get; set; }
    public int MaximumQuantity { get; set; }
}