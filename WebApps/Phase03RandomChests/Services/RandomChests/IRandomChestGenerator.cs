namespace Phase03RandomChests.Services.RandomChests;
public interface IRandomChestGenerator
{
    Task<RandomChestResultModel> GenerateRewardAsync();
}