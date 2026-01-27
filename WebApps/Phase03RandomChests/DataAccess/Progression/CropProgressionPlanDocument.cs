namespace Phase03RandomChests.DataAccess.Progression;
public class CropProgressionPlanDocument : IFarmDocumentModel, IFarmDocumentFactory<CropProgressionPlanDocument>
{
    required public FarmKey Farm { get; set; }
    // Total capacity that *exists* (even if locked)
    public BasicList<int> SlotLevelRequired { get; set; } = [];
    public BasicList<ItemUnlockRule> UnlockRules { get; set; } = [];
    public static CropProgressionPlanDocument CreateEmpty(FarmKey farm) => new()
    {
        Farm = farm
    };
}