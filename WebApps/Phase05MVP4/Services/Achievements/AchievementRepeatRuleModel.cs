namespace Phase05MVP4.Services.Achievements;
public class AchievementRepeatRuleModel
{
    public BasicList<int> FirstTargets { get; set; } = [];
    public int IncrementAfterFirst { get; set; }
}