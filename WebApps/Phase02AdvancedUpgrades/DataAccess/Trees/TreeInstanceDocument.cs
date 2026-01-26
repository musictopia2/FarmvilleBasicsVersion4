using Phase02AdvancedUpgrades.Services.Core;

namespace Phase02AdvancedUpgrades.DataAccess.Trees;
public class TreeInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<TreeAutoResumeModel> Trees { get; set; }
}