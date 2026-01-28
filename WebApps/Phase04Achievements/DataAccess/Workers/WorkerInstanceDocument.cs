namespace Phase04Achievements.DataAccess.Workers;
public class WorkerInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<UnlockModel> Workers { get; set; } = [];
}
