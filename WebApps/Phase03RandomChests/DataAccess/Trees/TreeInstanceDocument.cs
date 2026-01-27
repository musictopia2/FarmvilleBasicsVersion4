using Phase03RandomChests.Services.Core;

namespace Phase03RandomChests.DataAccess.Trees;
public class TreeInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<TreeAutoResumeModel> Trees { get; set; }
}