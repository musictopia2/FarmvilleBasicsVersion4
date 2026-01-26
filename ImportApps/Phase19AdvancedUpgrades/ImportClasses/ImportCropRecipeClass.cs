namespace Phase19AdvancedUpgrades.ImportClasses;
public static class ImportCropRecipeClass
{
    public static async Task ImportCropsAsync()
    {
        BasicList<CropRecipeDocument> list = [];
        list.AddRange(GetCountryRecipes());
        list.AddRange(GetTropicalRecipes());
        CropRecipeDatabase db = new();
        await db.ImportAsync(list);
    }
    private static BasicList<CropRecipeDocument> GetCountryRecipes()
    {
        string theme = FarmThemeList.Country;
        BasicList<CropRecipeDocument> output = [];
        BasicList<int> levels =
            [
                5, 8, 11
            ];
        output.Add(new CropRecipeDocument
        {
            Item = CountryItemList.Wheat,
            Duration = TimeSpan.FromSeconds(30),
            Theme = theme,
            TierLevelRequired = levels
        });
        levels =
            [
                7, 10, 13
            ];
        output.Add(new CropRecipeDocument
        {
            Item = CountryItemList.Corn,
            Duration = TimeSpan.FromMinutes(2),
            Theme = theme,
            TierLevelRequired = levels
        });
        levels =
            [
                10, 13, 16
            ];
        output.Add(new CropRecipeDocument
        {
            Item = CountryItemList.Carrot,
            Duration = TimeSpan.FromMinutes(4),
            Theme = theme,
            TierLevelRequired = levels
        });
        levels =
            [
                12, 15, 18
            ];
        output.Add(new CropRecipeDocument
        {
            Item = CountryItemList.Strawberry,
            Duration = TimeSpan.FromHours(1),
            Theme = theme,
            TierLevelRequired = levels
        });
        levels =
            [
                15, 18, 21
            ];
        output.Add(new CropRecipeDocument
        {
            Item = CountryItemList.Tomato,
            Duration = TimeSpan.FromMinutes(10),
            Theme = theme,
            TierLevelRequired = levels
        });
        levels =
            [
                23, 26, 29
            ];
        output.Add(new CropRecipeDocument
        {
            Item = CountryItemList.HoneyComb,
            Duration = TimeSpan.FromMinutes(45),
            Theme = theme,
            TierLevelRequired = levels
        });
        return output;
    }
    private static BasicList<CropRecipeDocument> GetTropicalRecipes()
    {
        string theme = FarmThemeList.Tropical;
        BasicList<CropRecipeDocument> output = [];
        BasicList<int> levels =
           [
               5, 8, 11
           ];
        output.Add(new CropRecipeDocument
        {
            Item = TropicalItemList.Pineapple,
            Duration = TimeSpan.FromSeconds(45),
            Theme = theme,
            TierLevelRequired= levels
        });
        levels =
            [
                7, 10, 13
            ];
        output.Add(new CropRecipeDocument
        {
            Item = TropicalItemList.Rice,
            Duration = TimeSpan.FromMinutes(1),
            Theme = theme,
            TierLevelRequired= levels
        });
        levels =
            [
                12, 15, 18
            ];
        output.Add(new CropRecipeDocument
        {
            Item = TropicalItemList.Tapioca,
            Duration = TimeSpan.FromMinutes(20),
            Theme = theme,
            TierLevelRequired= levels
        });
        return output;
    }
}