namespace Phase19AdvancedUpgrades.ImportClasses;
public static class ImportTreeRecipeClass
{
    public static async Task ImportTreesAsync()
    {
        BasicList<TreeRecipeDocument> list = [];
        list.AddRange(GetCountryRecipes());
        list.AddRange(GetTropicalRecipes());
        TreeRecipeDatabase db = new();
        await db.ImportAsync(list);
    }
    private static BasicList<TreeRecipeDocument> GetCountryRecipes()
    {
        string theme = FarmThemeList.Country;
        BasicList<TreeRecipeDocument> output = [];
        BasicList<int> levels =
            [
                5, 8, 11
            ];
        TreeRecipeDocument tree = new()
        {
            TreeName = CountryTreeListClass.Apple,
            Item = CountryItemList.Apple,
            ProductionTimeForEach = TimeSpan.FromSeconds(10),
            Theme = theme,
            TierLevelRequired= levels,
            IsFast = true
        };
        output.Add(tree);
        levels =
            [
                16, 19, 22
            ];
        tree = new()
        {
            TreeName = CountryTreeListClass.Peach,
            Item = CountryItemList.Peach,
            ProductionTimeForEach = TimeSpan.FromHours(1),
            Theme = theme,
            TierLevelRequired= levels,
            IsFast = false
        };
        output.Add(tree);
        return output;
    }
    private static BasicList<TreeRecipeDocument> GetTropicalRecipes()
    {
        string theme = FarmThemeList.Tropical;
        BasicList<TreeRecipeDocument> output = [];
        BasicList<int> levels =
           [
               5, 8, 11
           ];
        TreeRecipeDocument tree = new()
        {
            TreeName = TropicalTreeListClass.Coconut,
            Item = TropicalItemList.Coconut,
            ProductionTimeForEach = TimeSpan.FromMinutes(2),
            Theme = theme,
            TierLevelRequired= levels,
            IsFast = true
        };
        output.Add(tree);
        levels =
            [
                16, 19, 22
            ];
        tree = new()
        {
            TreeName = TropicalTreeListClass.Lime,
            Item = TropicalItemList.Lime,
            ProductionTimeForEach = TimeSpan.FromMinutes(45),
            Theme = theme,
            TierLevelRequired= levels,
            IsFast = false
        };
        output.Add(tree);
        return output;
    }
}