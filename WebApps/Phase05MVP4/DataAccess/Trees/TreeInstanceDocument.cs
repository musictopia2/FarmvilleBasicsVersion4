using Phase05MVP4.Services.Core;

namespace Phase05MVP4.DataAccess.Trees;
public class TreeInstanceDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    required public BasicList<TreeAutoResumeModel> Trees { get; set; }
}