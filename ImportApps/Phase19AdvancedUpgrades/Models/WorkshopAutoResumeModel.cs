namespace Phase19AdvancedUpgrades.Models;
public class WorkshopAutoResumeModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int SelectedRecipeIndex { get; set; } = 0;
    public string Name { get; set; } = "";
    public BasicList<UnlockModel> SupportedItems { get; set; } = [];
    //this is now needed because even if you can craft, maybe you had not purchased it yet.
    public bool Unlocked { get; set; } = true;
    public int Capacity { get; set; } = 0; //this time, start with 0.  let it calculate.
    public BasicList<CraftingAutoResumeModel> Queue { get; set; } = [];
    public double? RunMultiplier { get; set; }
    public TimeSpan ReduceBy { get; set; } = TimeSpan.Zero;
}