namespace Phase05MVP4.Services.TimedBoosts;
public class TimedBoostProfileModel
{
    public BasicList<TimedBoostCredit> Credits { get; set; } = [];

    // currently running boosts
    public BasicList<ActiveTimedBoost> Active { get; set; } = [];
}