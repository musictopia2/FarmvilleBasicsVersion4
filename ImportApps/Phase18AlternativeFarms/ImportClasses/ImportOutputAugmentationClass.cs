namespace Phase18AlternativeFarms.ImportClasses;
internal static class ImportOutputAugmentationClass
{
    public static async Task ImportOutputAugmentationAsync()
    {
        var farms = FarmHelperClass.GetAllBaselineFarms(); //actually without catalog, can't even fill in the details.
        BasicList<OutputAugmentationPlanDocument> list = [];
        foreach (var farm in farms)
        {
            BasicList<OutputAugmentationPlanModel> plans;

            if (farm.Theme == FarmThemeList.Country)
            {
                plans = await GetCountryAugmentationAsync(farm);
            }
            else if (farm.Theme == FarmThemeList.Tropical)
            {
                plans = await GetTropicalAugmentationAsync(farm);
            }
            else
            {
                throw new CustomBasicException($"The farm theme {farm.Theme} is not supported for augmentation import.");
            }

            list.Add(new()
            {
                Farm = farm,
                Items = plans // rename to Plans if your document uses Plans
            });
        }
        farms = FarmHelperClass.GetAllCoinFarms();
        list.AddRange(OutputAugmentationPlanDocument.PopulateEmptyForCoins());
        OutputAugmentationPlanDatabase db = new();
        await db.ImportAsync(list);
    }

    private static OutputAugmentationPlanModel Chance(string key, string target, BasicList<string> rewards) => new()
    {
        Key = key,
        TargetName = target,
        Rewards = rewards,
        ChancePercent = 50,
        IsDouble = false
    };

    private static OutputAugmentationPlanModel GuaranteedList(string key, string target, BasicList<string> rewards) => new()
    {
        Key = key,
        TargetName = target,
        Rewards = rewards,
        ChancePercent = 100,
        IsDouble = false
    };

    private static OutputAugmentationPlanModel GuaranteedDouble(string key, string target, string received) => new()
    {
        Key = key,
        TargetName = target,
        Rewards = FarmHelperClass.GetOnlyItem(received),
        ChancePercent = 100,
        IsDouble = true
    };

    private static CatalogOfferModel GetOffer(BasicList<CatalogOfferModel> offers, string key) =>
        offers.FirstOrDefault(x => x.OutputAugmentationKey == key)
        ?? throw new CustomBasicException($"Missing timed boost catalog offer for OutputAugmentationKey '{key}'.");

    private static async Task<BasicList<OutputAugmentationPlanModel>> GetCountryAugmentationAsync(FarmKey farm)
    {
        CatalogOfferDatabase db = new();
        var offers = await db.GetCatalogAsync(farm, EnumCatalogCategory.TimedBoost);
        BasicList<OutputAugmentationPlanModel> output = [];

        string key;
        string target;

        key = CountryAugmentationKeys.ApplePieChanceExtraApplePie;
        target = GetOffer(offers, key).TargetName;
        output.Add(Chance(key, target, FarmHelperClass.GetOnlyItem(target)));

        key = CountryAugmentationKeys.CowChanceButter;
        target = GetOffer(offers, key).TargetName;
        output.Add(Chance(key, target, FarmHelperClass.GetOnlyItem(CountryItemList.Butter)));

        key = CountryAugmentationKeys.MinesGuaranteedGranolaBlanket;
        target = GetOffer(offers, key).TargetName;
        output.Add(GuaranteedList(key, target, [CountryItemList.Blanket, CountryItemList.GranolaBar]));

        key = CountryAugmentationKeys.PeachTreeChanceExtraPeach;
        target = GetOffer(offers, key).TargetName;
        output.Add(Chance(key, target, FarmHelperClass.GetOnlyItem(target)));

        key = CountryAugmentationKeys.SheepDoubleWoolGuaranteed;
        target = GetOffer(offers, key).TargetName;
        output.Add(GuaranteedDouble(key, target, CountryItemList.Wool)); //fully tested.

        key = CountryAugmentationKeys.TomatoChanceFarmersSoup;
        target = GetOffer(offers, key).TargetName;
        output.Add(Chance(key, target, FarmHelperClass.GetOnlyItem(CountryItemList.FarmersSoup)));
        ValidateNoDuplicateTargets(output, farm);
        return output;
    }

    private static async Task<BasicList<OutputAugmentationPlanModel>> GetTropicalAugmentationAsync(FarmKey farm)
    {
        CatalogOfferDatabase db = new();
        var offers = await db.GetCatalogAsync(farm, EnumCatalogCategory.TimedBoost);
        BasicList<OutputAugmentationPlanModel> output = [];

        string key;
        string target;

        key = TropicalAugmentationKeys.ChickenDoubleEggsGuaranteed;
        target = GetOffer(offers, key).TargetName;
        output.Add(GuaranteedDouble(key, target, CountryItemList.Eggs));

        key = TropicalAugmentationKeys.DolphinChanceSearedFish;
        target = GetOffer(offers, key).TargetName;
        output.Add(Chance(key, target, FarmHelperClass.GetOnlyItem(TropicalItemList.SearedFish)));

        key = TropicalAugmentationKeys.GrilledCrabChanceExtraGrilledCrab;
        target = GetOffer(offers, key).TargetName;
        output.Add(Chance(key, target, FarmHelperClass.GetOnlyItem(target)));

        key = TropicalAugmentationKeys.LimeChanceExtraLime;
        target = GetOffer(offers, key).TargetName;
        output.Add(Chance(key, target, FarmHelperClass.GetOnlyItem(target)));

        key = TropicalAugmentationKeys.RiceChanceExtraSteamedRice;
        target = GetOffer(offers, key).TargetName;
        output.Add(Chance(key, target, FarmHelperClass.GetOnlyItem(TropicalItemList.SteamedRice)));

        key = TropicalAugmentationKeys.SmugglersCaveGuaranteedTruffleFriesAndFriedRice;
        target = GetOffer(offers, key).TargetName;
        output.Add(GuaranteedList(key, target, [TropicalItemList.TruffleFries, TropicalItemList.FriedRice]));
        ValidateNoDuplicateTargets(output, farm);
        return output;
    }



    private static void ValidateNoDuplicateTargets(BasicList<OutputAugmentationPlanModel> plans, FarmKey farm)
    {
        static string Normalize(string s) => (s ?? "").Trim().ToLowerInvariant();

        var dupes = plans
            .GroupBy(p => Normalize(p.TargetName))
            .Where(g => g.Count() > 1)
            .Select(g => new
            {
                Target = g.Key,
                Keys = g.Select(x => x.Key).ToList()
            })
            .ToList();

        if (dupes.Count == 0)
        {
            return;
        }

        // Build a useful error message so you can fix the import fast
        var lines = dupes.Select(d =>
            $"{d.Target} => {string.Join(", ", d.Keys)}");

        throw new CustomBasicException(
            $"Output augmentation import invalid for farm '{farm.PlayerName}' ({farm.Theme}). " +
            $"Each TargetName may appear only once. Duplicates: {string.Join(" | ", lines)}");
    }

}
