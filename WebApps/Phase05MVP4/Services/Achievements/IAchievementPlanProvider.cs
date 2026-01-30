namespace Phase05MVP4.Services.Achievements;
public interface IAchievementPlanProvider
{
    Task<BasicList<AchievementPlanModel>> GetPlanAsync(FarmKey farm);
}