namespace Phase04Achievements.DataAccess.Animals;
public class AnimalRecipeDocument
{
    public string Animal { get; init; } = "";
    public BasicList<AnimalProductionOption> Options { get; init; } = [];
    required public string Theme { get; init; }
    required public BasicList<int> TierLevelRequired { get; init; } = [];
    required public bool IsFast { get; init; }
}