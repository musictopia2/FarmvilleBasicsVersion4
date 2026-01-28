namespace Phase21Achievements.Models;
public class CropAutoResumeModel
{
    //try to do without id (?)

    //public Guid Id { get; set; } = Guid.NewGuid();  // Unique slot ID
    public string? Crop { get; set; }               // Crop planted (if any)
    public EnumCropState State { get; set; }       // Empty / Growing / Ready
    public DateTime? PlantedAt { get; set; } //when the crop was planted
    public bool Unlocked { get; set; } = true;             // Is this slot unlocked
    public double? RunMultiplier { get; set; }
    public TimeSpan ReducedBy { get; set; } = TimeSpan.Zero;
    public bool ExtrasResolved { get; set; }
    public OutputAugmentationSnapshot? OutputPromise { get; set; }
    public BasicList<ItemAmount> ExtraRewards { get; set; } = [];

    public double? AdvancedSpeedBonus { get; set; }
    //here may need something else to capture the extra stuff as well.
}