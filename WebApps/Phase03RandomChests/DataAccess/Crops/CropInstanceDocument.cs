namespace Phase03RandomChests.DataAccess.Crops;
public class CropInstanceDocument : IFarmDocumentModel
{
    required public BasicList<CropAutoResumeModel> Slots { get; set; } = [];
    required public BasicList<CropDataModel> Crops { get; set; } = [];
    required public FarmKey Farm { get; set; }
}