using Phase03RandomChests.Services.Core;

namespace Phase03RandomChests.DataAccess.Workshops;
public class WorkshopInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<WorkshopAutoResumeModel> Workshops { get; set; }
}