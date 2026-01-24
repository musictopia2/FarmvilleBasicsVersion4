using Phase01AlternativeFarms.Services.Core;

namespace Phase01AlternativeFarms.DataAccess.Worksites;
public class WorksiteInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<WorksiteAutoResumeModel> Worksites { get; set; } = [];
}