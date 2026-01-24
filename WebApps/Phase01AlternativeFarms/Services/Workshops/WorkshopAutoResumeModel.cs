namespace Phase01AlternativeFarms.Services.Workshops;
public class WorkshopAutoResumeModel
{
    public Guid Id { get; set; } //can be okay for the import but not for reals.
    public int SelectedRecipeIndex { get; set; } = 0;
    public string Name { get; set; } = "";
    public BasicList<UnlockModel> SupportedItems { get; set; } = [];
    public bool Unlocked { get; set; } //needs this too now.
    public int Capacity { get; set; } = 2;
    public BasicList<CraftingAutoResumeModel> Queue { get; set; } = [];
    public TimeSpan ReducedBy { get; set; } = TimeSpan.Zero;
    public bool IsRental { get; set; } //this means if it comes from rental, needs to mark so can lock the exact proper one.
}