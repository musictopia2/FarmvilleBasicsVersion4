using Phase04Achievements.Services.Core;

namespace Phase04Achievements.DataAccess.Worksites;
public class WorksiteInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<WorksiteAutoResumeModel> Worksites { get; set; } = [];
}