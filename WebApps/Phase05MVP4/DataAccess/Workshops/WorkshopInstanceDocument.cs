using Phase05MVP4.Services.Core;

namespace Phase05MVP4.DataAccess.Workshops;
public class WorkshopInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<WorkshopAutoResumeModel> Workshops { get; set; }
}