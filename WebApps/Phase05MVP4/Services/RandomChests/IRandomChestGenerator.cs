namespace Phase05MVP4.Services.RandomChests;
public interface IRandomChestGenerator
{
    Task<RandomChestResultModel> GenerateRewardAsync();
}