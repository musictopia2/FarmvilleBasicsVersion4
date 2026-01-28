namespace Phase04Achievements.DataAccess.Progression;
public class AnimalProgressionPlanDocument : IFarmDocumentModel, IFarmDocumentFactory<AnimalProgressionPlanDocument>
{
    required public FarmKey Farm { get; set; }
    public BasicList<ItemUnlockRule> UnlockRules { get; set; } = [];
    public static AnimalProgressionPlanDocument CreateEmpty(FarmKey farm)
    {
        return new()
        {
            Farm = farm
        };
    }
}