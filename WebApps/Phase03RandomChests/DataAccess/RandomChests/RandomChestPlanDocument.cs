namespace Phase03RandomChests.DataAccess.RandomChests;
public class RandomChestPlanDocument : IFarmDocumentModel, IFarmDocumentFactory<RandomChestPlanDocument>
{
    public required FarmKey Farm { get; init; }
    public BasicList<RandomChestCategoryWeightModel> CategoryWeights { get; set; } = [];
    public BasicList<RandomChestItemWeightModel> ItemWeights { get; set; } = [];
    public BasicList<RandomChestQuantityModel> QuantityRules { get; set; } = [];

    public static RandomChestPlanDocument CreateEmpty(FarmKey farm)
    {
        return new()
        {
            Farm = farm,
        };
    }
}