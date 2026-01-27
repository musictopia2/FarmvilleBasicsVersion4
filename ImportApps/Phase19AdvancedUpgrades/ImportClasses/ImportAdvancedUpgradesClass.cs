namespace Phase19AdvancedUpgrades.ImportClasses;
public static class ImportAdvancedUpgradesClass
{
    public static async Task ImportUpgradesAsync()
    {
        BasicList<FarmKey> farms = FarmHelperClass.GetAllBaselineFarms();
        BasicList<AdvancedUpgradePlanDocument> list = [];

        foreach (var farm in farms)
        {
            BasicList<AdvancedUpgradePlanModel> upgrades = GetUpgrades();
            

            list.Add(new()
            {
                Farm = farm,
                Upgrades = upgrades
            });
        }

        list.AddRange(AdvancedUpgradePlanDocument.PopulateEmptyForCoins());
        AdvancedUpgradePlanDatabase db = new();
        await db.ImportAsync(list);
    }
    private static BasicList<AdvancedUpgradePlanModel> GetUpgrades()
    {
        BasicList<AdvancedUpgradePlanModel> output = [];

        // there are 3 tiers for Standard/Fastest.
        // workshops have 5 tiers.
        BasicList<int> basicCosts = [2, 3, 4];
        BasicList<int> workshopCosts = [3, 4, 5];

        // SpeedBonus: 0 = none, 1 = instant (matches Country Escape semantics)
        BasicList<double> fastest = [0.3, 0.7, 1.0];
        BasicList<double> standard = [0.15, 0.35, 0.5];
        BasicList<double> workshopBoosts = [0.15, 0.25, 0.35, 0.45, 0.5];

        // Replace with your real upgrade cost item key
        const string upgradeCostKey = CurrencyKeys.Coin;

        // -----------------------------
        // Standard plan
        // -----------------------------
        {
            BasicList<AdvancedUpgradeTier> tiers = [];
            for (int i = 0; i < standard.Count; i++)
            {
                tiers.Add(new AdvancedUpgradeTier
                {
                    SpeedBonus = standard[i],
                    Cost = new Dictionary<string, int>
                    {
                        [upgradeCostKey] = basicCosts[i]
                    }
                });
            }

            output.Add(new AdvancedUpgradePlanModel
            {
                Category = EnumAdvancedUpgradeTrack.Standard,
                ExtraOutputChance = null,
                Tiers = tiers
            });
        }

        // -----------------------------
        // Fastest plan (exceptions)
        // -----------------------------
        {
            BasicList<AdvancedUpgradeTier> tiers = [];
            for (int i = 0; i < fastest.Count; i++)
            {
                tiers.Add(new AdvancedUpgradeTier
                {
                    SpeedBonus = fastest[i],
                    Cost = new Dictionary<string, int>
                    {
                        [upgradeCostKey] = basicCosts[i]
                    }
                });
            }

            output.Add(new AdvancedUpgradePlanModel
            {
                Category = EnumAdvancedUpgradeTrack.Fastest,
                ExtraOutputChance = null,
                Tiers = tiers
            });
        }

        // -----------------------------
        // Workshop plan (5 tiers)
        // -----------------------------
        {
            BasicList<AdvancedUpgradeTier> tiers = [];
            for (int i = 0; i < workshopBoosts.Count; i++)
            {
                int cost = i < workshopCosts.Count
                    ? workshopCosts[i]
                    : workshopCosts[^1]; // repeat last cost for tiers 4-5 (rebalance later)

                tiers.Add(new AdvancedUpgradeTier
                {
                    SpeedBonus = workshopBoosts[i],
                    Cost = new Dictionary<string, int>
                    {
                        [upgradeCostKey] = cost
                    }
                });
            }

            output.Add(new AdvancedUpgradePlanModel
            {
                Category = EnumAdvancedUpgradeTrack.Workshop,
                ExtraOutputChance = 50, //must be in integer above 1.
                Tiers = tiers
            });
        }

        return output;
    }


}
