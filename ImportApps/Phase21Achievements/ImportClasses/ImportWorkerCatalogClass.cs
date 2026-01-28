namespace Phase21Achievements.ImportClasses;
internal static class ImportWorkerCatalogClass
{
    private static EnumCatalogCategory _category = EnumCatalogCategory.Worker;
    private static TimeSpan _duration = TimeSpan.FromHours(2);
    public static BasicList<CatalogOfferModel> GetWorkerOffers(FarmKey farm)
    {
        if (farm.Theme == FarmThemeList.Tropical)
        {
            return GetCatalogForTropical();
        }
        if (farm.Theme == FarmThemeList.Country)
        {
            return GetCatalogForCountry();
        }
        throw new CustomBasicException("Not Supported");
    }
    private static BasicList<CatalogOfferModel> GetCatalogForTropical()
    {
        BasicList<CatalogOfferModel> output = [];
        output.Add(new()
        {
            TargetName = TropicalWorkerListClass.Toby,
            LevelRequired = 5,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10), //has to charge at least one or can't test the charging of a worker.
            Category = _category
        });
        output.Add(new()
        {
            TargetName = TropicalWorkerListClass.George,
            LevelRequired = 5,
            Costs = FarmHelperClass.GetFreeCosts,
            Category= _category
        });
        output.Add(new()
        {
            TargetName = TropicalWorkerListClass.Zazu,
            LevelRequired = 6,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(15),
            Category = _category
        });
        output.Add(new()
        {
            TargetName = TropicalWorkerListClass.Kai,
            LevelRequired = 10,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20), 
            Duration = _duration,
            Category = _category
        });
        output.Add(new()
        {
            TargetName = TropicalWorkerListClass.Ethan,
            LevelRequired = 12,
            Costs = FarmHelperClass.GetFreeCosts,
            Category = _category
        });
        output.Add(new()
        {
            TargetName = TropicalWorkerListClass.Luna,
            LevelRequired = 12,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(15),
            Category = _category
        });
        output.Add(new()
        {
            TargetName = TropicalWorkerListClass.Fiona,
            LevelRequired = 18,
            Costs = FarmHelperClass.GetFreeCosts,
            Category = _category
        });
        output.Add(new()
        {
            TargetName = TropicalWorkerListClass.Kilo,
            LevelRequired = 18,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20),
            Duration = _duration,
            Category = _category
        });
        return output;
    }
    private static BasicList<CatalogOfferModel> GetCatalogForCountry()
    {
        BasicList<CatalogOfferModel> output = [];
        output.Add(new()
        {
            TargetName = CountryWorkerListClass.Bob,
            LevelRequired = 7,
            Costs = FarmHelperClass.GetFreeCosts,
            Category = _category
        });
        output.Add(new()
        {
            TargetName = CountryWorkerListClass.Alice,
            LevelRequired = 9,
            Costs = FarmHelperClass.GetFreeCosts,
            Category = _category
        });
        
        output.Add(new()
        {
            TargetName = CountryWorkerListClass.Clara,
            LevelRequired = 12,
            Costs = FarmHelperClass.GetFreeCosts,
            Category = _category
        });
        output.Add(new()
        {
            TargetName = CountryWorkerListClass.Whiskers,
            LevelRequired = 12,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10),
            Duration = _duration,
            Category = _category
        });
        output.Add(new()
        {
            TargetName = CountryWorkerListClass.Baxter,
            LevelRequired = 12,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10),
            Duration = _duration,
            Category = _category
        });
        output.Add(new()
        {
            TargetName = CountryWorkerListClass.Mittens,
            LevelRequired = 12,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20),
            Category = _category
        });
        output.Add(new()
        {
            TargetName = CountryWorkerListClass.Daniel,
            LevelRequired = 16,
            Costs = FarmHelperClass.GetFreeCosts,
            Category = _category
        });
        output.Add(new()
        {
            TargetName = CountryWorkerListClass.Emma,
            LevelRequired = 18,
            Costs = FarmHelperClass.GetFreeCosts,
            Category = _category
        });
        output.Add(new()
        {
            TargetName = CountryWorkerListClass.Rusty,
            LevelRequired = 16,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20),
            Category = _category
        });
        
        return output;
    }
}