namespace Phase20RandomChests.ImportClasses;
internal static class ImportWorkshopCatalogClass
{
    private readonly static EnumCatalogCategory _category = EnumCatalogCategory.Workshop;
    private readonly static TimeSpan _duration = TimeSpan.FromHours(2);
    public static BasicList<CatalogOfferModel> GetWorkshopOffers(FarmKey farm)
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
            TargetName = TropicalWorkshopList.HuluHit,
            LevelRequired = 2,
            Category = _category,
            Costs = FarmHelperClass.GetFreeCosts
        });
        output.Add(new()
        {
            TargetName = TropicalWorkshopList.HuluHit,
            LevelRequired = 4,
            Category = _category,
            Duration = _duration,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20) //this is where i do the balancing.
        });
        
        output.Add(new()
        {
            TargetName = TropicalWorkshopList.SushiStand,
            LevelRequired = 4,
            Category = _category,
            Costs = FarmHelperClass.GetFreeCosts
        });
        output.Add(new()
        {
            TargetName = TropicalWorkshopList.SushiStand,
            LevelRequired = 5,
            Category = _category,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20),
            Duration = _duration
        });
        output.Add(new()
        {
            TargetName = TropicalWorkshopList.Grill,
            LevelRequired = 5,
            Category = _category,
            Costs = FarmHelperClass.GetFreeCosts
        });

        output.Add(new()
        {
            TargetName = TropicalWorkshopList.Grill,
            LevelRequired = 6,
            Category = _category,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20),
            Duration = _duration
        });

        output.Add(new()
        {
            TargetName = TropicalWorkshopList.BeachfrontKitchen,
            LevelRequired = 11,
            Category = _category,
            Costs = FarmHelperClass.GetFreeCosts
        });
        output.Add(new()
        {
            TargetName = TropicalWorkshopList.BeachfrontKitchen,
            LevelRequired = 12,
            Category = _category,
            Duration = _duration,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20) //this is where i do the balancing.
        });
        return output;
    }
    private static BasicList<CatalogOfferModel> GetCatalogForCountry()
    {
        BasicList<CatalogOfferModel> output = [];
        output.Add(new()
        {
            TargetName = CountryWorkshopList.Windmill,
            LevelRequired = 2,
            Category = _category,
            Costs = FarmHelperClass.GetFreeCosts
        });
        output.Add(new()
        {
            TargetName = CountryWorkshopList.Windmill,
            LevelRequired = 3,
            Category = _category,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10),
            Duration = _duration
        });
        
        output.Add(new()
        {
            TargetName = CountryWorkshopList.PastryOven,
            LevelRequired = 4,
            Category = _category,
            Costs = FarmHelperClass.GetFreeCosts
        });
        output.Add(new()
        {
            TargetName = CountryWorkshopList.PastryOven,
            LevelRequired = 5,
            Category = _category,
            Duration = _duration,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10)
        });

        output.Add(new()
        {
            TargetName = CountryWorkshopList.Dairy,
            LevelRequired = 9,
            Category = _category,
            Costs = FarmHelperClass.GetFreeCosts
        });
        output.Add(new()
        {
            TargetName = CountryWorkshopList.Dairy,
            LevelRequired = 10,
            Category = _category,
            Duration = _duration,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10)
        });
       

        output.Add(new()
        {
            TargetName = CountryWorkshopList.StovetopOven,
            LevelRequired = 11,
            Category = _category,
            Costs = FarmHelperClass.GetFreeCosts
        });
        output.Add(new()
        {
            TargetName = CountryWorkshopList.StovetopOven,
            LevelRequired = 12,
            Category = _category,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10),
            Duration = _duration
        });
        
        output.Add(new()
        {
            TargetName = CountryWorkshopList.Loom,
            LevelRequired = 14,
            Category = _category,
            Costs = FarmHelperClass.GetFreeCosts
        });
        output.Add(new()
        {
            TargetName = CountryWorkshopList.Loom,
            LevelRequired = 15,
            Category = _category,
            Duration = _duration,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10)
        });
        return output;
    }
}