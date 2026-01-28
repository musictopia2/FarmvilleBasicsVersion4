namespace Phase04Achievements.DataAccess.Items;
public class ItemPlanDocument : IFarmDocumentModel
{
    public required FarmKey Farm { get; init; }
    public BasicList<ItemPlanModel> ItemList { get; set; } = []; //this is a list of all possible items period.
}