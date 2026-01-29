namespace Phase04Achievements.Services.Achievements;
public interface IAchievementProfile
{
    Task<AchievementProfileModel> LoadAsync();
    Task SaveAsync(AchievementProfileModel profile);
}