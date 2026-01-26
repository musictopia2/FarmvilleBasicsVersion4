using Phase02AdvancedUpgrades.Services.Core;

namespace Phase02AdvancedUpgrades.DataAccess.Workshops;
public class WorkshopInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<WorkshopAutoResumeModel> Workshops { get; set; }
}