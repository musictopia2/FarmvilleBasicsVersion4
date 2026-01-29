namespace Phase04Achievements.Services.Achievements;
public interface IAchievementFactory
{
    AchievementServicesContext GetAchievementServices(FarmKey farm);
}