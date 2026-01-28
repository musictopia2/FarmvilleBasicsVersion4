namespace Phase20RandomChests.ImportClasses;
public static class ImportRandomChestClass
{
    public static async Task ImportRandomChestsAsync()
    {
        var farms = FarmHelperClass.GetAllBaselineFarms();
        BasicList<RandomChestPlanDocument> list = [];
        foreach (var farm in farms)
        {
            if (farm.Theme == FarmThemeList.Country)
            {
                list.Add(PopulateCountry(farm));
            }
            else if (farm.Theme == FarmThemeList.Tropical)
            {
                list.Add(PopulateTropical(farm));
            }
            else
            {
                throw new CustomBasicException("Not Supported");
            }
        }
        list.AddRange(RandomChestPlanDocument.PopulateEmptyForCoins());
        RandomChestPlanDatabase db = new();
        await db.ImportAsync(list);

    }
    private static RandomChestPlanDocument PopulateCountry(FarmKey farm)
    {
        var categories = GetMainCategories();
        var quantities = GetQuantities();
        BasicList<RandomChestItemWeightModel> items = [];
        items.AddRange(GetSharedItems());
        //other code
        string category = RandomChestKeys.PowerPin;
        TimeSpan duration = TimeSpan.FromHours(1); //can adjust
        items.Add(new()
        {
            Category = category,
            Duration = duration,
            ReducedBy = TimeSpan.FromMinutes(3),
            TargetName = CountryItemList.Carrot,
            LeveRequired = 2,
            ItemWeight = 5
        });
        items.Add(new()
        {
            Category = category,
            Duration = duration,
            ReducedBy = TimeSpan.FromMinutes(8),
            TargetName = CountryItemList.Tomato,
            LeveRequired = 2,
            ItemWeight = 5
        });
        
        return new()
        {
            Farm = farm,
            CategoryWeights = categories,
            QuantityRules = quantities,
            ItemWeights = items
        };
    }
    private static RandomChestPlanDocument PopulateTropical(FarmKey farm)
    {
        var categories = GetMainCategories();
        var quantities = GetQuantities();
        BasicList<RandomChestItemWeightModel> items = [];
        items.AddRange(GetSharedItems());
        //other code
        string category = RandomChestKeys.PowerPin;
        TimeSpan duration = TimeSpan.FromHours(1); //can adjust
        items.Add(new()
        {
            Category = category,
            Duration = duration,
            TargetName = TropicalItemList.Egg,
            LeveRequired = 2,
            ReducedBy = TimeSpan.FromMinutes(1),
            ItemWeight = 5
        });
        items.Add(new()
        {
            Category = category,
            Duration = duration,
            TargetName = TropicalItemList.Rice,
            LeveRequired = 2,
            ReducedBy = TimeSpan.FromSeconds(40),
            ItemWeight = 5
        });
        return new()
        {
            Farm = farm,
            CategoryWeights = categories,
            QuantityRules = quantities,
            ItemWeights = items
        };
    }
    private static BasicList<RandomChestCategoryWeightModel> GetMainCategories()
    {
        BasicList<RandomChestCategoryWeightModel> output = [];
        output.Add(new()
        {
            Key = RandomChestKeys.Coin,
            Weight = 10
        });
        output.Add(new()
        {
            Key = RandomChestKeys.SpeedSeed,
            Weight = 20,
            LevelRequired = 2
        });
        output.Add(new()
        {
            Key = RandomChestKeys.WorksiteNoSupplies,
            Weight = 6,
            LevelRequired = 4
        });
        output.Add(new()
        {
            Key = RandomChestKeys.UnlimitedSpeedSeed,
            Weight = 1,
            LevelRequired = 4
        });
        output.Add(new()
        {
            Key = RandomChestKeys.PowerGlove,
            Weight = 30,
            LevelRequired = 3
        });
        output.Add(new()
        {
            Key = RandomChestKeys.FinishSingle,
            Weight = 20,
            LevelRequired = 4
        });
        output.Add(new()
        {
            Key = RandomChestKeys.FinishAll,
            Weight = 10,
            LevelRequired = 4
        });
        output.Add(new()
        {
            Key = RandomChestKeys.PowerPin,
            Weight = 15,
            LevelRequired = 11
        });
        return output;
    }
    private static BasicList<RandomChestQuantityModel> GetQuantities()
    {
        BasicList<RandomChestQuantityModel> output = [];
        output.Add(new()
        {
            MinimumQuantity = 5,
            MaximumQuantity = 20,
            TargetName = CurrencyKeys.Coin
        });
        output.Add(new()
        {
            MinimumQuantity = 10,
            MaximumQuantity = 20,
            TargetName = CurrencyKeys.SpeedSeed
        });
        output.Add(new()
        {
            MinimumQuantity = 5,
            MaximumQuantity = 10,
            TargetName = CurrencyKeys.PowerGloveWorkshop
        });
        output.Add(new()
        {
            MinimumQuantity = 3,
            MaximumQuantity = 5,
            TargetName = CurrencyKeys.PowerGloveWorksite
        });
        output.Add(new()
        {
            MinimumQuantity = 1,
            MaximumQuantity = 2,
            TargetName = CurrencyKeys.FinishAllWorkshops
        });
        //worksites only 1.
        output.Add(new()
        {
            MinimumQuantity = 4,
            MaximumQuantity = 8,
            TargetName = CurrencyKeys.FinishSingleWorkshop
        });
        output.Add(new()
        {
            MinimumQuantity = 1,
            MaximumQuantity = 3,
            TargetName = CurrencyKeys.FinishSingleWorksite
        });

        return output;
    }
    private static BasicList<RandomChestItemWeightModel> GetSharedItems()
    {
        BasicList<RandomChestItemWeightModel> output = [];
        output.Add(new()
        {
            Category = RandomChestKeys.PowerPin,
            TargetName = CurrencyKeys.PowerGloveWorkshop,
            ItemWeight = 5,
            LeveRequired = 3
        });
        output.Add(new()
        {
            Category = RandomChestKeys.PowerPin,
            TargetName = CurrencyKeys.PowerGloveWorkshop,
            ItemWeight = 2,
            LeveRequired = 3
        });

        output.Add(new()
        {
            Category = RandomChestKeys.FinishSingle,
            TargetName = CurrencyKeys.FinishSingleWorkshop,
            ItemWeight = 5,
            LeveRequired = 4
        });
        output.Add(new()
        {
            Category = RandomChestKeys.FinishSingle,
            TargetName = CurrencyKeys.FinishSingleWorksite,
            ItemWeight = 2,
            LeveRequired = 4
        });

        output.Add(new()
        {
            Category = RandomChestKeys.FinishAll,
            TargetName = CurrencyKeys.FinishAllWorkshops,
            ItemWeight = 5,
            LeveRequired = 4
        });
        output.Add(new()
        {
            Category = RandomChestKeys.FinishAll,
            TargetName = CurrencyKeys.FinishAllWorksites,
            ItemWeight = 2,
            LeveRequired = 4
        });


        return output;
    }

}