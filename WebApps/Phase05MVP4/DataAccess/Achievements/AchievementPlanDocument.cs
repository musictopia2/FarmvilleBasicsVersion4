namespace Phase05MVP4.DataAccess.Achievements;
public class AchievementPlanDocument : IFarmDocumentModel, IFarmDocumentFactory<AchievementPlanDocument>
{
    public required FarmKey Farm { get; set; }
    public BasicList<AchievementPlanModel> AchievementPlans { get; set; } = []; //this is everything needed for the achievements.
    public static AchievementPlanDocument CreateEmpty(FarmKey farm)
    {
        return new()
        {
            Farm = farm
        };
    }
}