namespace Phase21Achievements.ImportClasses;
public static class ImportAchievementPlanClass
{
    public static async Task ImportAchievementsAsync()
    {
        BasicList<FarmKey> farms = FarmHelperClass.GetAllMainFarms();
        BasicList<AchievementPlanDocument> list = [];
        foreach (var farm in farms)
        {
            list.Add(PopulateAchievements(farm));
        }
        list.AddRange(AchievementPlanDocument.PopulateEmptyForCoins());
        list.AddRange(AchievementPlanDocument.PopulateEmptyForCoop());
        AchievementPlanDatabase db = new();
        await db.ImportAsync(list);
    }
    private static BasicList<AchievementPlanModel> PopulateSharedAchievements()
    {
        BasicList<AchievementPlanModel> output = [];
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.Level,
            Target = 34,
            CoinReward = 50
        }); //you will receive in extra 50 coin for getting to level 34 (for testing).

        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.SpendCoin,
            RepeatAchievementRules = new()
            {
                FirstTargets = [500, 1200],
                IncrementAfterFirst = 1000
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [20, 40],
                CoinIncrementAfterFirst = 20
            }
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.CompleteScenarios,
            RepeatAchievementRules = new()
            {
                FirstTargets = [1, 2],
                IncrementAfterFirst = 5
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [20, 40],
                CoinIncrementAfterFirst = 20
            }
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.CoinEarned,
            CoinReward = 100,
            Target = 1000,

        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.CompleteOrders,
            RepeatAchievementRules = new()
            {
                FirstTargets = [2, 5],
                IncrementAfterFirst = 12
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [5, 8],
                CoinIncrementAfterFirst = 9
            }
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.UseTimedBoost,
            CoinReward = 1,
            Target = 1,
            SourceKey = BoostKeys.UnlimitedSpeedSeed
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.UseTimedBoost,
            CoinReward = 2,
            Target = 1,
            SourceKey = BoostKeys.WorksiteNoSupplies
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.UseConsumable,
            ItemKey = CurrencyKeys.SpeedSeed,
            RepeatAchievementRules = new()
            {
                FirstTargets = [2, 5],
                IncrementAfterFirst = 12
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [5, 8],
                CoinIncrementAfterFirst = 9
            }
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.UseConsumable,
            ItemKey = CurrencyKeys.PowerGloveWorkshop,
            RepeatAchievementRules = new()
            {
                FirstTargets = [2, 5],
                IncrementAfterFirst = 12
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [5, 8],
                CoinIncrementAfterFirst = 1
            }
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.UseConsumable,
            ItemKey = CurrencyKeys.FinishSingleWorkshop,
            RepeatAchievementRules = new()
            {
                FirstTargets = [1, 2],
                IncrementAfterFirst = 4
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [2, 4],
                CoinIncrementAfterFirst = 1
            }
        });
       
        return output;
    }

    private static void Validate(AchievementPlanModel a)
    {
        bool repeatable = a.RepeatAchievementRules is not null;

        if (repeatable)
        {
            if (a.RepeatRewardRules is null)
            {
                throw new CustomBasicException($"Repeatable achievement '{a.CounterKey}' missing RepeatRewardRules.");
            }

            if (a.Target is not null || a.CoinReward is not null)
            {
                throw new CustomBasicException($"Repeatable achievement '{a.CounterKey}' should not have Target/CoinReward.");
            }
        }
        else
        {
            if (a.Target is null || a.CoinReward is null)
            {
                throw new CustomBasicException($"Non-repeatable achievement '{a.CounterKey}' must have Target and CoinReward.");
            }
        }
    }

    private static BasicList<AchievementPlanModel> PopulateCountryAchievements()
    {
        BasicList<AchievementPlanModel> output = [];
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.UseTimedBoost,
            //SourceKey = CountryItemList.ApplePie,
            OutputAugmentationKey = CountryAugmentationKeys.ApplePieChanceExtraApplePie, //if you use any with this pin, then would work.
            CoinReward = 1,
            Target = 2
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.UseTimedBoost,
            //SourceKey = CountryItemList.Wool,
            OutputAugmentationKey = CountryAugmentationKeys.SheepDoubleWoolGuaranteed, //if you use any with this pin, then would work.
            RepeatAchievementRules = new()
            {
                FirstTargets = [1, 2],
                IncrementAfterFirst = 4
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [2, 4],
                CoinIncrementAfterFirst = 1
            }
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.UseTimedBoost,
            SourceKey = CountryWorksiteListClass.GrandmasGlade, //duration does not matter or the reduced by does not matter.
            RepeatAchievementRules = new()
            {
                FirstTargets = [1, 2],
                IncrementAfterFirst = 4
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [2, 4],
                CoinIncrementAfterFirst = 1
            }
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.UseTimedBoost,
            SourceKey = CountryWorksiteListClass.Mines, //duration does not matter or the reduced by does not matter.
            CoinReward = 1,
            Target = 2
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.CompleteOrders,
            ItemKey = CountryItemList.Wheat,
            RepeatAchievementRules = new()
            {
                FirstTargets = [1, 2],
                IncrementAfterFirst = 4
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [2, 4],
                CoinIncrementAfterFirst = 1
            }
        });

        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.CollectFromAnimal,
            SourceKey = CountryAnimalListClass.Cow,
            RepeatAchievementRules = new()
            {
                FirstTargets = [10, 20],
                IncrementAfterFirst = 50
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [2, 4],
                CoinIncrementAfterFirst = 3
            }
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.CollectFromAnimal,
            SourceKey = CountryAnimalListClass.Goat,
            CoinReward = 2,
            Target = 2
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.CraftFromWorkshops,
            SourceKey = CountryWorkshopList.Windmill,
            ItemKey = CountryItemList.Wheat,
            RepeatAchievementRules = new()
            {
                FirstTargets = [10, 20],
                IncrementAfterFirst = 50
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [2, 4],
                CoinIncrementAfterFirst = 3
            }
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.CraftFromWorkshops,
            SourceKey = CountryWorkshopList.Windmill,
            ItemKey = CountryItemList.Sugar,
            CoinReward = 2,
            Target = 2
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.FindFromWorksites,
            SourceKey = CountryWorksiteListClass.Pond,
            ItemKey = CountryItemList.Bass,
            CoinReward = 1,
            Target = 2
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.FindFromWorksites,
            SourceKey = CountryWorksiteListClass.GrandmasGlade,
            ItemKey = CountryItemList.Blackberry,
            RepeatAchievementRules = new()
            {
                FirstTargets = [1, 2],
                IncrementAfterFirst = 4
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [2, 4],
                CoinIncrementAfterFirst = 1
            }
        });

        return output;
    }
    private static BasicList<AchievementPlanModel> PopulateTropicalAchievements()
    {
        BasicList<AchievementPlanModel> output = [];
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.UseTimedBoost,
            OutputAugmentationKey = TropicalAugmentationKeys.GrilledCrabChanceExtraGrilledCrab, //if you use any with this pin, then would work.
            CoinReward = 1,
            Target = 2
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.UseTimedBoost,
            OutputAugmentationKey = TropicalAugmentationKeys.ChickenDoubleEggsGuaranteed, //if you use any with this pin, then would work.
            RepeatAchievementRules = new()
            {
                FirstTargets = [1, 2],
                IncrementAfterFirst = 4
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [2, 4],
                CoinIncrementAfterFirst = 1
            }
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.UseTimedBoost,
            SourceKey = TropicalWorksiteListClass.CorelReef, //duration does not matter or the reduced by does not matter.
            RepeatAchievementRules = new()
            {
                FirstTargets = [1, 2],
                IncrementAfterFirst = 4
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [2, 4],
                CoinIncrementAfterFirst = 1
            }
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.UseTimedBoost,
            SourceKey = TropicalWorksiteListClass.SmugglersCave, //duration does not matter or the reduced by does not matter.
            CoinReward = 1,
            Target = 2
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.CompleteOrders,
            ItemKey = TropicalItemList.Pineapple,
            RepeatAchievementRules = new()
            {
                FirstTargets = [1, 2],
                IncrementAfterFirst = 4
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [2, 4],
                CoinIncrementAfterFirst = 1
            }
        });

        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.CollectFromAnimal,
            SourceKey = TropicalAnimalListClass.Dolphin,
            RepeatAchievementRules = new()
            {
                FirstTargets = [10, 20],
                IncrementAfterFirst = 50
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [2, 4],
                CoinIncrementAfterFirst = 3
            }
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.CollectFromAnimal,
            SourceKey = TropicalAnimalListClass.Boar,
            CoinReward = 2,
            Target = 2
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.CraftFromWorkshops,
            SourceKey = TropicalWorkshopList.HuluHit,
            ItemKey = TropicalItemList.PineappleSmoothie,
            RepeatAchievementRules = new()
            {
                FirstTargets = [10, 20],
                IncrementAfterFirst = 50
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [2, 4],
                CoinIncrementAfterFirst = 3
            }
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.CraftFromWorkshops,
            SourceKey = TropicalWorkshopList.HuluHit,
            ItemKey = TropicalItemList.PinaColada,
            CoinReward = 2,
            Target = 2
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.FindFromWorksites,
            SourceKey = TropicalWorksiteListClass.HotSprings,
            ItemKey = TropicalItemList.Vanilla,
            CoinReward = 1,
            Target = 2
        });
        output.Add(new()
        {
            CounterKey = AchievementCounterKeys.FindFromWorksites,
            SourceKey = TropicalWorksiteListClass.CorelReef,
            ItemKey = TropicalItemList.Seashell,
            RepeatAchievementRules = new()
            {
                FirstTargets = [1, 2],
                IncrementAfterFirst = 4
            },
            RepeatRewardRules = new()
            {
                FirstCoinRewards = [2, 4],
                CoinIncrementAfterFirst = 1
            }
        });
        return output;
    }
    private static AchievementPlanDocument PopulateAchievements(FarmKey farm)
    {
        BasicList<AchievementPlanModel> output = [];
        output.AddRange(PopulateSharedAchievements());
        if (farm.Theme == FarmThemeList.Tropical)
        {
            output.AddRange(PopulateTropicalAchievements());
        }
        else if (farm.Theme == FarmThemeList.Country)
        {
            output.AddRange(PopulateCountryAchievements());
        }
        else
        {
            throw new CustomBasicException("Not Supported");
        }
        foreach (var item in output)
        {
            Validate(item);
        }
        ValidateNoDuplicates(output, farm);
        AchievementPlanDocument doc = new()
        {
            Farm = farm,
            AchievementPlans = output
        };
        return doc;
    }


    private static string GetUniquenessKey(AchievementPlanModel a)
    {
        return $"{a.CounterKey}|{a.SourceKey}|{a.ItemKey}|{a.OutputAugmentationKey}";
    }
    private static void ValidateNoDuplicates(BasicList<AchievementPlanModel> output, FarmKey farm)
    {
        var groups = output
            .GroupBy(GetUniquenessKey)
            .Where(g => g.Count() > 1)
            .ToList();

        if (groups.Count == 0)
        {
            return;
        }

        // Build a helpful error message showing which one duplicated
        var msg = string.Join(Environment.NewLine, groups.Select(g =>
        {
            var sample = g.First();
            return $"Duplicate achievement in farm '{farm}': " +
                   $"CounterKey='{sample.CounterKey}', SourceKey='{sample.SourceKey}', ItemKey='{sample.ItemKey}', OutputAugmentationKey='{sample.OutputAugmentationKey}' (count={g.Count()})";
        }));

        throw new CustomBasicException(msg);
    }

}