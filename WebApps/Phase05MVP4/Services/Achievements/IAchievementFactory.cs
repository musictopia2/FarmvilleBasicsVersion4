namespace Phase05MVP4.Services.Achievements;
public interface IAchievementFactory
{
    AchievementServicesContext GetAchievementServices(FarmKey farm);
}