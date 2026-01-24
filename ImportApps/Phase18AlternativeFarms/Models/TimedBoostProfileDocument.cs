namespace Phase18AlternativeFarms.Models;
public sealed class TimedBoostProfileDocument : IFarmDocumentModel, IFarmDocumentFactory<TimedBoostProfileDocument>
{
    public required FarmKey Farm { get; set; }

    // credits you own (can activate later)
    public BasicList<TimedBoostCredit> Credits { get; set; } = [];

    // currently running boosts
    public BasicList<ActiveTimedBoost> Active { get; set; } = [];

    public static TimedBoostProfileDocument CreateEmpty(FarmKey farm)
    {
        return new()
        {
            Farm = farm
        };
    }
}