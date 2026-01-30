namespace Phase05MVP4.Services.Achievements;
public interface IAchievementProfile
{
    Task<AchievementProfileModel> LoadAsync();
    Task SaveAsync(AchievementProfileModel profile);
}