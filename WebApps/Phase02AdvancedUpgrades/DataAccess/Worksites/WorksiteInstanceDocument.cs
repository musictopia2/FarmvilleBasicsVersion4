using Phase02AdvancedUpgrades.Services.Core;

namespace Phase02AdvancedUpgrades.DataAccess.Worksites;
public class WorksiteInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<WorksiteAutoResumeModel> Worksites { get; set; } = [];
}