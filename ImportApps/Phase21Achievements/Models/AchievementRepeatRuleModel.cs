namespace Phase21Achievements.Models;
public class AchievementRepeatRuleModel
{
    public BasicList<int> FirstTargets { get; set; } = [];
    public int IncrementAfterFirst { get; set; }
}