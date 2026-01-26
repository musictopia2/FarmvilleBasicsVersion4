namespace Phase19AdvancedUpgrades.DataAccess;
public static class ImportTimedBoostProfileClass
{
    private static CatalogOfferDatabase _catalogOfferDatabase = null!;
    public static async Task ImportTimedBoostsAsync()
    {
        _catalogOfferDatabase = new();
        BasicList<TimedBoostProfileDocument> list = [];
        var firsts = FarmHelperClass.GetAllBaselineFarms();
        foreach (var item in firsts)
        {
            list.Add(await CreateInstanceAsync(item));
        }
        list.AddRange(TimedBoostProfileDocument.PopulateEmptyForCoins()); //i assume the coin farm will not do timed boosts.
        TimedBoostProfileDatabase db = new();
        await db.ImportAsync(list);
    }
    private static async Task<TimedBoostProfileDocument> CreateInstanceAsync(FarmKey farm)
    {
        BasicList<TimedBoostCredit> list = [];
        var plan = await _catalogOfferDatabase.GetCatalogAsync(farm, EnumCatalogCategory.TimedBoost);
        plan.ForConditionalItems(x => x.Costs.Count == 0, catalog =>
        {
            if (catalog.Duration.HasValue == false)
            {
                throw new CustomBasicException("All timed boosts must have a duration");
            }
            list.Add(new()
            {
                BoostKey = catalog.TargetName,
                Duration = catalog.Duration.Value,
                Quantity = 2,
                ReduceBy = catalog.ReduceBy,
                OutputAugmentationKey = catalog.OutputAugmentationKey
            });
        });
        return new TimedBoostProfileDocument
        {
            Farm = farm,
            Credits = list
        };
    }
}