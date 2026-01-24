using Phase01AlternativeFarms.Services.Core;

namespace Phase01AlternativeFarms.DataAccess.Workshops;
public class WorkshopInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<WorkshopAutoResumeModel> Workshops { get; set; }
}