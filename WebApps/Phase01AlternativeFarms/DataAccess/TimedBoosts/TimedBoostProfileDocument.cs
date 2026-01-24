namespace Phase01AlternativeFarms.DataAccess.TimedBoosts;
public sealed class TimedBoostProfileDocument : IFarmDocument
{
    public required FarmKey Farm { get; set; }

    // credits you own (can activate later)
    public BasicList<TimedBoostCredit> Credits { get; set; } = [];

    // currently running boosts
    public BasicList<ActiveTimedBoost> Active { get; set; } = [];
}