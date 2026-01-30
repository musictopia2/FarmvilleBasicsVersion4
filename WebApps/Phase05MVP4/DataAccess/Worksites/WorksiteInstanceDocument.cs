using Phase05MVP4.Services.Core;

namespace Phase05MVP4.DataAccess.Worksites;
public class WorksiteInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<WorksiteAutoResumeModel> Worksites { get; set; } = [];
}