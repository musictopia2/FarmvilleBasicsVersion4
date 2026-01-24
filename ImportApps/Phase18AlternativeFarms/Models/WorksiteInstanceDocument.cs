namespace Phase18AlternativeFarms.Models;
public class WorksiteInstanceDocument
{
    required public FarmKey Farm { get; set; }
    required public BasicList<WorksiteAutoResumeModel> Worksites { get; set; } = [];
}