namespace Phase21Achievements.Models;
public class OutputAugmentationPlanDocument : IFarmDocumentModel, IFarmDocumentFactory<OutputAugmentationPlanDocument>
{
    public FarmKey Farm { get; set; }
    public BasicList<OutputAugmentationPlanModel> Items { get; set; } = new();
    public static OutputAugmentationPlanDocument CreateEmpty(FarmKey farm)
    {
        return new()
        {
            Farm = farm
        };
    }
}