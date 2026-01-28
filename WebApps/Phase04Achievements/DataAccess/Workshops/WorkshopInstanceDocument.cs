using Phase04Achievements.Services.Core;

namespace Phase04Achievements.DataAccess.Workshops;
public class WorkshopInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<WorkshopAutoResumeModel> Workshops { get; set; }
}