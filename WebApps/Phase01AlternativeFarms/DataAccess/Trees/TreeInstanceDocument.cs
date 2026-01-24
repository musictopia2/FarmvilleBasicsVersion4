using Phase01AlternativeFarms.Services.Core;

namespace Phase01AlternativeFarms.DataAccess.Trees;
public class TreeInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<TreeAutoResumeModel> Trees { get; set; }
}