namespace Phase01AlternativeFarms.DataAccess.Progression;
public class WorkshopProgressionPlanDocument : IFarmDocumentModel, IFarmDocumentFactory<WorkshopProgressionPlanDocument>
{
    required public FarmKey Farm { get; set; }
    public BasicList<ItemUnlockRule> UnlockRules { get; set; } = [];
    public static WorkshopProgressionPlanDocument CreateEmpty(FarmKey farm)
    {
        return new()
        {
            Farm = farm
        };
    }
}