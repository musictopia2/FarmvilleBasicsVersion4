namespace Phase03RandomChests.DataAccess.Animals;
public class AnimalInstanceDocument : IFarmDocumentModel
{
    required public BasicList<AnimalAutoResumeModel> Animals { get; set; }
    required public FarmKey Farm { get; set; }
}