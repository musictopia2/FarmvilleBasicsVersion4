namespace Phase21Achievements.Models;
public class CropDataModel
{
    public string Item { get; set; } = "";
    public bool Unlocked { get; set; }
    public bool IsSuppressed { get; set; }
    public int Level { get; set; } = 1; //needed so can do a lookup for their upgrades.
    public double? AdvancedSpeedBonus { get; set; }
    public bool MaxBenefits { get; set; }
}