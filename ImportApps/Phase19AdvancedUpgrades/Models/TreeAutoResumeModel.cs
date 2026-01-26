namespace Phase19AdvancedUpgrades.Models;
public class TreeAutoResumeModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    required public string TreeName { get; set; } = "";
    public bool Unlocked { get; set; } = true;
    public int TreesReady { get; set; } = 4; //i think.
    public EnumTreeState State { get; set; } = EnumTreeState.Collecting;
    public DateTime? StartedAt { get; set; }
    public DateTime? TempStart { get; set; }
    public double? RunMultiplier { get; set; }
    public TimeSpan ReducedBy { get; set; } = TimeSpan.Zero;

    // Temporary override: something else is taking over the rule, so this tree shouldn't appear / be usable
    public bool IsSuppressed { get; set; } = false;
    public OutputAugmentationSnapshot? OutputPromise { get; set; }
    public bool IsRental { get; set; } //this means if it comes from rental, needs to mark so can lock the exact proper one.
    public bool RentalExpired { get; set; }
    public int Level { get; set; } = 1;
    public double? AdvancedSpeedBonus { get; set; }
    public bool MaxBenefits { get; set; }
}