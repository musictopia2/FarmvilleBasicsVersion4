namespace Phase18AlternativeFarms.Models;
public class OutputAugmentationPlanDocument : IFarmDocumentModel
{
    public FarmKey Farm { get; set; }
    public BasicList<OutputAugmentationPlanModel> Items { get; set; } = new();
}