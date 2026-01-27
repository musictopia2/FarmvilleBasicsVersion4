using Phase03RandomChests.Services.Core;

namespace Phase03RandomChests.DataAccess.Worksites;
public class WorksiteInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<WorksiteAutoResumeModel> Worksites { get; set; } = [];
}