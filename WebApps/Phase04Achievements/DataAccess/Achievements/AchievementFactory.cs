namespace Phase04Achievements.DataAccess.Achievements;
public class AchievementFactory : IAchievementFactory
{
    AchievementServicesContext IAchievementFactory.GetAchievementServices(FarmKey farm)
    {
        return new()
        {
            AchievementPlanProvider = new AchievementPlanDatabase(),
            AchievementProfile = new AchievementProfileDatabase(farm)
        };
    }
}