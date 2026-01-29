namespace Phase21Achievements.Models;
public class AchievementRepeatRewardRuleModel
{
    public BasicList<int> FirstCoinRewards { get; set; } = [];  // e.g. [5, 8]
    public int CoinIncrementAfterFirst { get; set; }       // e.g. +2 each tier after
}