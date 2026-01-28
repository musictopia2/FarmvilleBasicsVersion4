namespace Phase21Achievements.ImportClasses;
public static class ImportWorkshopAdvancedRulesClass
{
    public static async Task ImportWorkshopsAsync()
    {
        //figure out the workshops level requirements.
        //has to guess for this.
        //since i only support 20 levels, then sometimes, i am forced to allow sooner than normal.
        //for testing, allow much faster.
        //this is part of balancing as well.
        BasicList<WorkshopAdvancedUpgradeRuleDocument> list = [];
        list.AddRange(GetCountryRules());
        list.AddRange(GetTropicalRules());
        WorkshopAdvancedUpgradeRuleDatabase db = new();
        await db.ImportAsync(list);
    }

    private static BasicList<WorkshopAdvancedUpgradeRuleDocument> GetCountryRules()
    {
        BasicList<WorkshopAdvancedUpgradeRuleDocument> output = [];
        BasicList<int> levels =
            [
                6, 9, 12, 15, 18
            ];
        output.Add(new()
        {
            BuildingName = CountryWorkshopList.Windmill,
            Theme = FarmThemeList.Country,
            TierLevelRequired = levels
        });
        levels =
            [
                9, 12, 15, 18, 21
            ];
        output.Add(new()
        {
            BuildingName = CountryWorkshopList.PastryOven,
            Theme = FarmThemeList.Country,
            TierLevelRequired = levels
        });
        levels =
            [
                15, 18, 21, 24, 27
            ];
        output.Add(new()
        {
            BuildingName = CountryWorkshopList.Dairy,
            Theme = FarmThemeList.Country,
            TierLevelRequired = levels
        });
        levels =
            [
                19, 22, 25, 28, 31
            ];
        //needs 35 levels now.
        output.Add(new()
        {
            BuildingName = CountryWorkshopList.StovetopOven,
            Theme = FarmThemeList.Country,
            TierLevelRequired = levels
        });
        levels =
            [
                21, 24, 27, 30, 33
            ];
        output.Add(new()
        {
            BuildingName = CountryWorkshopList.Loom,
            Theme = FarmThemeList.Country,
            TierLevelRequired = levels
        });
        return output;
    }
    private static BasicList<WorkshopAdvancedUpgradeRuleDocument> GetTropicalRules()
    {
        BasicList<WorkshopAdvancedUpgradeRuleDocument> output = [];
        BasicList<int> levels =
            [
                6, 9, 12, 15, 18
            ];
        output.Add(new()
        {
            BuildingName = TropicalWorkshopList.HuluHit,
            Theme = FarmThemeList.Tropical,
            TierLevelRequired = levels
        });
        levels =
            [
                9, 12, 15, 18, 21
            ];
        output.Add(new()
        {
            BuildingName = TropicalWorkshopList.SushiStand,
            Theme = FarmThemeList.Tropical,
            TierLevelRequired = levels
        });
        levels =
            [
                15, 18, 21, 24, 27
            ];
        output.Add(new()
        {
            BuildingName = TropicalWorkshopList.Grill,
            Theme = FarmThemeList.Tropical,
            TierLevelRequired = levels
        });
        levels =
            [
                21, 24, 27, 30, 33
            ];
        //needs 35 levels now.
        output.Add(new()
        {
            BuildingName = TropicalWorkshopList.BeachfrontKitchen,
            Theme = FarmThemeList.Country,
            TierLevelRequired = levels
        });

        //we may have a new building (with a few new recipes) (?)


        return output;
    }

}