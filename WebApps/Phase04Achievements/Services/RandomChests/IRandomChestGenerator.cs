namespace Phase04Achievements.Services.RandomChests;
public interface IRandomChestGenerator
{
    Task<RandomChestResultModel> GenerateRewardAsync();
}