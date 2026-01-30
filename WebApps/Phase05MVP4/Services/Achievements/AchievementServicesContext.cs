namespace Phase05MVP4.Services.Achievements;
public class AchievementServicesContext
{
    public required IAchievementPlanProvider AchievementPlanProvider { get; set; }
    public required IAchievementProfile AchievementProfile { get; set; }
}