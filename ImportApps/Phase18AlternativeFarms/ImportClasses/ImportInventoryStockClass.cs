namespace Phase18AlternativeFarms.ImportClasses;
public static class ImportInventoryStockClass
{
    private static CropProgressionPlanDatabase _cropProgression = null!;
    private static ProgressionProfileDatabase _levelProfile = null!;
    public static async Task ImportBeginningInventoryAmountsAsync()
    {
        BasicList<InventoryStockDocument> list = [];
        var farms = FarmHelperClass.GetAllBaselineFarms();
        _levelProfile = new();
        _cropProgression = new();
        foreach (var farm in farms)
        {
            list.Add(await GetBaselineInventoryAsync(farm));
        }
        farms = FarmHelperClass.GetAllCoinFarms();
        CropRecipeDatabase recipeDb = new();
        var recipes = await recipeDb.GetRecipesAsync();
        foreach (var farm in farms)
        {
            list.Add(GetCoinInventory(farm, recipes));
        }
        InventoryStockDatabase db = new();
        await db.ImportAsync(list);
    }
    private static InventoryStockDocument GetCoinInventory(FarmKey farm, BasicList<CropRecipeDocument> recipes)
    {
        Dictionary<string, int> amounts = [];
        recipes.ForConditionalItems(x => x.Theme == farm.Theme, recipe =>
        {
            amounts.Add(recipe.Item, 10);
        });
        //if i give extras, decide how many i get for the scenario.
        amounts.Add(CurrencyKeys.SpeedSeed, 10);
        amounts.Add(CurrencyKeys.FinishSingleWorksite, 3); //choose wisely when to use it.
        amounts.Add(CurrencyKeys.FinishSingleWorkshop, 3);
        amounts.Add(CurrencyKeys.FinishAllWorksites, 1);
        amounts.Add(CurrencyKeys.FinishAllWorkshops, 1);
        amounts.Add(CurrencyKeys.PowerGloveWorkshop, 5);
        amounts.Add(CurrencyKeys.PowerGloveWorksite, 5);
        return new()
        {
            Farm = farm,
            Baseline = amounts,
            Current = amounts
        };
    }
    private static async Task<InventoryStockDocument> GetBaselineInventoryAsync(FarmKey farm)
    {
        Dictionary<string, int> amounts = [];

        var p = await _levelProfile.GetProfileAsync(farm);
        int level = p.Level;
        CropProgressionPlanDocument crop = await _cropProgression.GetPlanAsync(farm);
        //var firsts = crop.UnlockRules.Where(x => level => x.LevelRequired)

        crop.UnlockRules.ForConditionalItems(x => level >= x.LevelRequired, rule =>
        {
            amounts.Add(rule.ItemName, 10);
            //amounts[rule.ItemName] = 10;
        });
        amounts.Add(CurrencyKeys.Coin, 3000);

        amounts.Add(CurrencyKeys.SpeedSeed, 40); //to get you up until you are able to use them.
        amounts.Add(CurrencyKeys.PowerGloveWorkshop, 10);
        amounts.Add(CurrencyKeys.PowerGloveWorksite, 2);
        amounts.Add(CurrencyKeys.FinishSingleWorkshop, 4);
        amounts.Add(CurrencyKeys.FinishSingleWorksite, 2);
        amounts.Add(CurrencyKeys.FinishAllWorksites, 1);
        amounts.Add(CurrencyKeys.FinishAllWorkshops, 2);
        return new()
        {
            Farm = farm,
            Current = amounts
        };
    }

}