namespace Phase19AdvancedUpgrades.DataAccess;
public static class ImportCatalogOfferClass
{
    public static async Task ImportCatalogAsync()
    {
        CatalogOfferDatabase db = new();
        var firsts = FarmHelperClass.GetAllBaselineFarms(); //only baseline farms.
        BasicList<CatalogOfferDocument> list = [];
        foreach (var item in firsts)
        {
            list.Add(GenerateCatalogFarm(item));
        }
        firsts = FarmHelperClass.GetAllCoinFarms();
        foreach (var item in firsts)
        {
            list.Add(new()
            {
                Farm = item,
                Offers = []
            });
        }
        await db.ImportAsync(list);
    }

    private static CatalogOfferDocument GenerateCatalogFarm(FarmKey farm)
    {
        BasicList<CatalogOfferModel> list = [];
        list.AddRange(ImportTreeCatalogClass.GetTreeOffers(farm));
        list.AddRange(ImportAnimalCatalogClass.GetAnimalOffers(farm));
        list.AddRange(ImportWorksiteCatalogClass.GetWorksiteOffers(farm));
        list.AddRange(ImportWorkerCatalogClass.GetWorkerOffers(farm));
        list.AddRange(ImportWorkshopCatalogClass.GetWorkshopOffers(farm));
        list.AddRange(ImportSpeedSeedOffers());
        list.AddRange(ImportPowerGloveOffers());
        list.AddRange(ImportUnlimitedSpeedSeeds());
        list.AddRange(ImportNoWorksiteSuppliesNeeded());
        list.AddRange(ImportSingleCompletionOffers());
        list.AddRange(ImportAllCompletionOffers());
        if (farm.Theme == FarmThemeList.Country)
        {
            list.AddRange(ImportCountryUnlimitedItems());
            list.AddRange(ImportCountryTimeReductionPowerPins());
            list.AddRange(ImportCountryOutputAugmentationOffers());
        }
        else if (farm.Theme == FarmThemeList.Tropical)
        {
            list.AddRange(ImportTropicalUnlimitedItems());
            list.AddRange(ImportTropicalTimeReductionPowerPins());
            list.AddRange(ImportTropicalOutputAugmentationOffers());
        }
        else
        {
            throw new CustomBasicException("Not supported");
        }
        return new()
        {
            Farm = farm,
            Offers = list

        };
    }
    private static BasicList<CatalogOfferModel> ImportNoWorksiteSuppliesNeeded()
    {
        EnumCatalogCategory category = EnumCatalogCategory.TimedBoost;
        BasicList<CatalogOfferModel> output = [];
        //TimeSpan duration = TimeSpan.FromHours(2);
        string boostKey = BoostKeys.WorksiteNoSupplies;
        output.Add(new()
        {
            Category = category,
            TargetName = boostKey,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(2),
            LevelRequired = 4,
            Duration = TimeSpan.FromHours(2)
        });
        
        return output;
    }
    private static BasicList<CatalogOfferModel> ImportCountryOutputAugmentationOffers()
    {
        EnumCatalogCategory category = EnumCatalogCategory.TimedBoost;
        BasicList<CatalogOfferModel> output = [];
        TimeSpan duration = TimeSpan.FromHours(2);
        output.Add(new()
        {
            Category = category,
            TargetName = CountryWorksiteListClass.Mines,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(2),
            Duration = duration,
            OutputAugmentationKey = CountryAugmentationKeys.MinesGuaranteedGranolaBlanket,
            LevelRequired = 18
        });

        output.Add(new()
        {
            Category = category,
            TargetName = CountryAnimalListClass.Sheep, //should give the animal name this time.
            Costs = FarmHelperClass.GetCoinOnlyDictionary(2),
            Duration = duration,
            OutputAugmentationKey = CountryAugmentationKeys.SheepDoubleWoolGuaranteed,
            LevelRequired = 14
        });

        output.Add(new()
        {
            Category = category,
            TargetName = CountryItemList.Peach,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(2),
            Duration = duration,
            OutputAugmentationKey = CountryAugmentationKeys.PeachTreeChanceExtraPeach,
            LevelRequired = 8
        });

        output.Add(new()
        {
            Category = category,
            TargetName = CountryItemList.ApplePie, //i think this time needs to be apple pies.
            Costs = FarmHelperClass.GetCoinOnlyDictionary(2),
            Duration = duration,
            OutputAugmentationKey = CountryAugmentationKeys.ApplePieChanceExtraApplePie,
            LevelRequired = 2
        });

        output.Add(new()
        {
            Category = category,
            TargetName = CountryAnimalListClass.Cow,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(2),
            Duration = duration,
            OutputAugmentationKey = CountryAugmentationKeys.CowChanceButter,
            LevelRequired = 10
        });

        output.Add(new()
        {
            Category = category,
            TargetName = CountryItemList.Tomato,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(2),
            Duration = duration,
            OutputAugmentationKey = CountryAugmentationKeys.TomatoChanceFarmersSoup,
            LevelRequired = 11
        });


        return output;
    }

    private static BasicList<CatalogOfferModel> ImportTropicalOutputAugmentationOffers()
    {
        EnumCatalogCategory category = EnumCatalogCategory.TimedBoost;
        BasicList<CatalogOfferModel> output = [];
        TimeSpan duration = TimeSpan.FromHours(2);
        output.Add(new()
        {
            Category = category,
            TargetName = TropicalWorksiteListClass.SmugglersCave,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(2),
            Duration = duration,
            OutputAugmentationKey = TropicalAugmentationKeys.SmugglersCaveGuaranteedTruffleFriesAndFriedRice,
            LevelRequired = 18
        });

        output.Add(new()
        {
            Category = category,
            TargetName = TropicalItemList.Rice, //should give the animal name this time.
            Costs = FarmHelperClass.GetCoinOnlyDictionary(2),
            Duration = duration,
            OutputAugmentationKey = TropicalAugmentationKeys.RiceChanceExtraSteamedRice,
            LevelRequired = 3
        });

        output.Add(new()
        {
            Category = category,
            TargetName = TropicalItemList.Lime,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(2),
            Duration = duration,
            OutputAugmentationKey = TropicalAugmentationKeys.LimeChanceExtraLime,
            LevelRequired = 4
        });

        output.Add(new()
        {
            Category = category,
            TargetName = TropicalItemList.GrilledCrab, //i think this time needs to be apple pies.
            Costs = FarmHelperClass.GetCoinOnlyDictionary(2),
            Duration = duration,
            OutputAugmentationKey = TropicalAugmentationKeys.GrilledCrabChanceExtraGrilledCrab,
            LevelRequired = 5
        });

        output.Add(new()
        {
            Category = category,
            TargetName = TropicalAnimalListClass.Dolphin,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(2),
            Duration = duration,
            OutputAugmentationKey = TropicalAugmentationKeys.DolphinChanceSearedFish,
            LevelRequired = 4
        });

        output.Add(new()
        {
            Category = category,
            TargetName = TropicalAnimalListClass.Chicken,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(2),
            Duration = duration,
            OutputAugmentationKey = TropicalAugmentationKeys.ChickenDoubleEggsGuaranteed,
            LevelRequired = 5
        });
        return output;
    }
    private static BasicList<CatalogOfferModel> ImportAllCompletionOffers()
    {
        BasicList<CatalogOfferModel> output = [];
        EnumCatalogCategory category = EnumCatalogCategory.Misc;
        int levelRequired = 4;
        output.Add(new()
        {
            Category = category,
            Quantity = 2,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10),
            TargetName = CurrencyKeys.FinishAllWorkshops,
            LevelRequired = levelRequired,

        });
        

        output.Add(new()
        {
            Category = category,
            Quantity = 2,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(15),
            TargetName = CurrencyKeys.FinishAllWorksites,
            LevelRequired = levelRequired,

        });
        

        return output;
    }
    private static BasicList<CatalogOfferModel> ImportSingleCompletionOffers()
    {
        BasicList<CatalogOfferModel> output = [];
        EnumCatalogCategory category = EnumCatalogCategory.Misc;
        int levelRequired = 4;
        output.Add(new()
        {
            Category = category,
            Quantity = 10,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10),
            TargetName = CurrencyKeys.FinishSingleWorkshop,
            LevelRequired = levelRequired,

        });
        output.Add(new()
        {
            Category = category,
            Quantity = 20,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20), //you pay less per item if doing bulk.
            TargetName = CurrencyKeys.FinishSingleWorkshop,
            LevelRequired = levelRequired

        });

        output.Add(new()
        {
            Category = category,
            Quantity = 10,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(15),
            TargetName = CurrencyKeys.FinishSingleWorksite,
            LevelRequired = levelRequired,

        });
        output.Add(new()
        {
            Category = category,
            Quantity = 20,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20), //you pay less per item if doing bulk.
            TargetName = CurrencyKeys.FinishSingleWorksite,
            LevelRequired = levelRequired

        });

        return output;
    }
    private static BasicList<CatalogOfferModel> ImportPowerGloveOffers()
    {
        BasicList<CatalogOfferModel> output = [];
        EnumCatalogCategory category = EnumCatalogCategory.Misc;
        int levelRequired = 3;
        output.Add(new()
        {
            Category = category,
            Quantity = 4,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(15),
            TargetName = CurrencyKeys.PowerGloveWorkshop,
            LevelRequired = levelRequired,

        });
        
        output.Add(new()
        {
            Category = category,
            Quantity = 4,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(15),
            TargetName = CurrencyKeys.PowerGloveWorksite,
            LevelRequired = levelRequired,

        });
        return output;
    }
    private static BasicList<CatalogOfferModel> ImportSpeedSeedOffers()
    {
        BasicList<CatalogOfferModel> output = [];
        EnumCatalogCategory category = EnumCatalogCategory.Misc;
        int levelRequired = 2;
        
        output.Add(new()
        {
            Category = category,
            Quantity = 50,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20), //you pay less per item if doing bulk.
            TargetName = CurrencyKeys.SpeedSeed,
            LevelRequired = levelRequired

        });
        return output;
    }

    private static BasicList<CatalogOfferModel> ImportUnlimitedSpeedSeeds()
    {
        BasicList<CatalogOfferModel> output = [];
        EnumCatalogCategory category = EnumCatalogCategory.TimedBoost;
        output.Add(new()
        {
            Category = category,
            LevelRequired = 4,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10),
            Duration = TimeSpan.FromMinutes(5),
            TargetName = BoostKeys.UnlimitedSpeedSeed
        });

        
        return output;
    }

    private static BasicList<CatalogOfferModel> ImportCountryTimeReductionPowerPins()
    {
        EnumCatalogCategory category = EnumCatalogCategory.TimedBoost;
        BasicList<CatalogOfferModel> output = [];
        TimeSpan duration = TimeSpan.FromHours(2);


        output.Add(new()
        {
            Category = category,
            TargetName = CountryItemList.Wheat,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(5),
            Duration = TimeSpan.FromMinutes(2),
            ReduceBy = TimeSpan.FromSeconds(20),
            LevelRequired = 1
        });
        output.Add(new()
        {
            Category = category,
            TargetName = CountryItemList.Corn,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(5),
            Duration = TimeSpan.FromMinutes(4),
            ReduceBy = TimeSpan.FromMinutes(1.4),
            LevelRequired = 2
        });
        output.Add(new()
        {
            Category = category,
            TargetName = CountryWorksiteListClass.Pond,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(5),
            Duration = duration,
            ReduceBy = TimeSpan.FromHours(7),
            LevelRequired = 11
        });
        output.Add(new()
        {
            Category = category,
            TargetName = CountryWorksiteListClass.Mines,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(5),
            Duration = duration,
            ReduceBy = TimeSpan.FromHours(1.5),
            LevelRequired = 14
        });
        output.Add(new()
        {
            Category = category,
            TargetName = CountryWorksiteListClass.GrandmasGlade,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(5),
            Duration = duration,
            ReduceBy = TimeSpan.FromMinutes(9),
            LevelRequired = 4
        });
        output.Add(new()
        {
            Category = category,
            TargetName = CountryItemList.Peach,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(5),
            Duration = duration,
            ReduceBy = TimeSpan.FromHours(3.5), //this applies to all 4 (so do math to see how it would affect each one).
            LevelRequired = 5
        });
        output.Add(new()
        {
            Category = category,
            TargetName = CountryWorkshopList.Loom,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(5),
            Duration = duration,
            ReduceBy = TimeSpan.FromMinutes(5),
            LevelRequired = 12
        });
        output.Add(new()
        {
            Category = category,
            TargetName = CountryItemList.Strawberry,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(5),
            Duration = duration,
            ReduceBy = TimeSpan.FromMinutes(52),
            LevelRequired = 7
        });
        output.Add(new()
        {
            Category = category,
            TargetName = CountryItemList.HoneyComb,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(5),
            Duration = duration,
            ReduceBy = TimeSpan.FromMinutes(40),
            LevelRequired = 12
        });
        output.Add(new()
        {
            Category = category,
            TargetName = CountryItemList.Wool,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(5),
            Duration = duration,
            ReduceBy = TimeSpan.FromMinutes(25),
            LevelRequired = 13
        });


        return output;
    }

    private static BasicList<CatalogOfferModel> ImportTropicalTimeReductionPowerPins()
    {
        EnumCatalogCategory category = EnumCatalogCategory.TimedBoost;
        BasicList<CatalogOfferModel> output = [];
        TimeSpan duration = TimeSpan.FromHours(2);
        output.Add(new()
        {
            Category = category,
            TargetName = TropicalWorksiteListClass.HotSprings,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(5),
            Duration = duration,
            ReduceBy = TimeSpan.FromHours(1.5),
            LevelRequired = 12
        });
        output.Add(new()
        {
            Category = category,
            TargetName = TropicalItemList.Lime,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(5),
            Duration = duration,
            ReduceBy = TimeSpan.FromHours(2.5), //this applies to all 4 (so do math to see how it would affect each one).
            LevelRequired = 13
        });
        output.Add(new()
        {
            Category = category,
            TargetName = TropicalWorkshopList.BeachfrontKitchen,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(5),
            Duration = duration,
            ReduceBy = TimeSpan.FromMinutes(5),
            LevelRequired = 8
        });
        output.Add(new()
        {
            Category = category,
            TargetName = TropicalItemList.Tapioca,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(5),
            Duration = duration,
            ReduceBy = TimeSpan.FromMinutes(16),
            LevelRequired = 4
        });
        output.Add(new()
        {
            Category = category,
            TargetName = TropicalItemList.Mushroom,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(5),
            Duration = duration,
            ReduceBy = TimeSpan.FromMinutes(1),
            LevelRequired = 4
        });
        return output;
    }

    private static BasicList<CatalogOfferModel> ImportCountryUnlimitedItems()
    {
        //this is where i set the prices
        BasicList<CatalogOfferModel> output = [];
        EnumCatalogCategory category = EnumCatalogCategory.InstantUnlimited;

        //has to start with only trees to get this working.  then apply to the other domains.

        output.Add(new()
        {
            Category = category,
            LevelRequired = 3,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10),
            TargetName = CountryItemList.Wheat
        });

        output.Add(new()
        {
            Category = category,
            LevelRequired = 3,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10),
            TargetName = CountryItemList.Apple
        });
        output.Add(new()
        {
            Category = category,
            LevelRequired = 4,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20),
            TargetName = CountryItemList.Milk,
        });

        output.Add(new()
        {
            Category = category,
            LevelRequired = 8,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20),
            Duration = TimeSpan.FromSeconds(20),
            TargetName = CountryItemList.Strawberry,
        });

        return output;
    }
    private static BasicList<CatalogOfferModel> ImportTropicalUnlimitedItems()
    {
        //this is where i set the prices
        BasicList<CatalogOfferModel> output = [];
        EnumCatalogCategory category = EnumCatalogCategory.InstantUnlimited;
        output.Add(new()
        {
            Category = category,
            LevelRequired = 3,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10),
            TargetName = TropicalItemList.Pineapple
        });
        output.Add(new()
        {
            Category = category,
            LevelRequired = 3,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(10),
            TargetName = TropicalItemList.Coconut
        });
        output.Add(new()
        {
            Category = category,
            LevelRequired = 4,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20),
            TargetName = TropicalItemList.Fish,
        });
        output.Add(new()
        {
            Category = category,
            LevelRequired = 7,
            Costs = FarmHelperClass.GetCoinOnlyDictionary(20),
            Duration = TimeSpan.FromSeconds(10),
            TargetName = TropicalItemList.Lime,
        });
        return output;
    }
}
