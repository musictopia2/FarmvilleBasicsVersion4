namespace Phase20RandomChests.ImportClasses;
public static class ImportAnimalCatalogClass
{
    private readonly static EnumCatalogCategory _category = EnumCatalogCategory.Animal;
    public static BasicList<CatalogOfferModel> GetAnimalOffers(FarmKey farm)
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
        TimeSpan duration = TimeSpan.FromHours(1);
        output.Add(new()
        {
            TargetName = TropicalAnimalListClass.Dolphin,
            LevelRequired = 2,
            Category = _category,
            Costs = FarmHelperClass.GetFreeCosts
        });
        output.Add(new()
        {
            TargetName = TropicalAnimalListClass.Dolphin,
            LevelRequired = 4,
            Category = _category,
            Duration = duration,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20) //this is where i do the balancing.
        });
        output.Add(new()
        {
            TargetName = TropicalAnimalListClass.Chicken,
            LevelRequired = 4,
            Category = _category,
            Costs = FarmHelperClass.GetFreeCosts
        });
        output.Add(new()
        {
            TargetName = TropicalAnimalListClass.Chicken,
            LevelRequired = 5,
            Category = _category,
            Duration = duration,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20) //this is where i do the balancing.
        });
        
        output.Add(new()
        {
            TargetName = TropicalAnimalListClass.Boar,
            LevelRequired = 11,
            Category = _category,
            Costs = FarmHelperClass.GetFreeCosts
        });
        output.Add(new()
        {
            TargetName = TropicalAnimalListClass.Boar,
            LevelRequired = 12,
            Category = _category,
            Duration = duration,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20) //this is where i do the balancing.
        });
        return output;
    }
    private static BasicList<CatalogOfferModel> GetCatalogForCountry()
    {
        BasicList<CatalogOfferModel> output = [];
        TimeSpan duration = TimeSpan.FromHours(1);
        output.Add(new()
        {
            TargetName = CountryAnimalListClass.Cow,
            LevelRequired = 3,
            Category = _category,
            Costs = FarmHelperClass.GetFreeCosts
        });
        output.Add(new()
        {
            TargetName = CountryAnimalListClass.Cow,
            LevelRequired = 4,
            Category = _category,
            Duration = duration,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10)
        });


        output.Add(new()
        {
            TargetName = CountryAnimalListClass.Goat,
            LevelRequired = 10,
            Category = _category,
            Costs = FarmHelperClass.GetFreeCosts
        });
        output.Add(new()
        {
            TargetName = CountryAnimalListClass.Goat,
            LevelRequired = 11,
            Category = _category,
            Duration = duration,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10)
        });
        
        output.Add(new()
        {
            TargetName = CountryAnimalListClass.Sheep,
            LevelRequired = 14,
            Category = _category,
            Costs = FarmHelperClass.GetFreeCosts
        });
        output.Add(new()
        {
            TargetName = CountryAnimalListClass.Sheep,
            LevelRequired = 15,
            Category = _category,
            Duration = duration,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10)
        });
        return output;
    }
}