namespace Phase21Achievements.Models;
public class AdvancedUpgradePlanDocument : IFarmDocumentModel, IFarmDocumentFactory<AdvancedUpgradePlanDocument>
{
    public required FarmKey Farm { get; init; }
    public BasicList<AdvancedUpgradePlanModel> Upgrades { get; set; } = [];
    public static AdvancedUpgradePlanDocument CreateEmpty(FarmKey farm)
    {
        return new()
        {
            Farm = farm,
        };
    }
}