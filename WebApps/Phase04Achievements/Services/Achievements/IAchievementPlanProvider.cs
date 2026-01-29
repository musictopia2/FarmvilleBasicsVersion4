namespace Phase04Achievements.Services.Achievements;
public interface IAchievementPlanProvider
{
    Task<BasicList<AchievementPlanModel>> GetPlanAsync(FarmKey farm);
}