using Phase04Achievements.Services.Core;

namespace Phase04Achievements.DataAccess.Trees;
public class TreeInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<TreeAutoResumeModel> Trees { get; set; }
}