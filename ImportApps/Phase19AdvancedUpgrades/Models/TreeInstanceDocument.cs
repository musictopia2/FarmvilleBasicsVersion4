namespace Phase19AdvancedUpgrades.Models;
public class TreeInstanceDocument
{
    required public FarmKey Farm { get; set; }
    required public BasicList<TreeAutoResumeModel> Trees { get; set; }
}