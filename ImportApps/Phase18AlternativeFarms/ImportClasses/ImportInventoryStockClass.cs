namespace Phase18AlternativeFarms.ImportClasses;
public static class ImportInventoryStockClass
{
    private static CropProgressionPlanDatabase _cropProgression = null!;
    private static ProgressionProfileDatabase _levelProfile = null!;
    public static async Task ImportBeginningInventoryAmountsAsync()
    {
        BasicList<InventoryStockDocument> list = [];
        var farms = FarmHelperClass.GetAllFarms();
        ProgressionProfileDatabase t = new();
        _levelProfile = new();
        _cropProgression = new();
        foreach (var farm in farms)
        {
            list.Add(await GetInventoryAsync(farm));
        }
       
        InventoryStockDatabase db = new();
        await db.ImportAsync(list);
    }

    private static async Task<InventoryStockDocument> GetInventoryAsync(FarmKey farm)
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
        //amounts.Add(CurrencyKeys.PowerGloveWorksite, 20);
        //amounts.Add(CurrencyKeys.PowerGloveWorkshop, 20);

        //if (farm.Theme == FarmThemeList.Country)
        //{
        //    amounts.Add(CountryItemList.Flour, 20);
        //    amounts.Add(CountryItemList.Milk, 10);
        //    amounts.Add(CountryItemList.Apple, 60); //so i can apple pies now.
        //}

        //amounts.Add(CurrencyKeys.SpeedSeed, 10); //you get 10 speed seeds.  once gone, that is it.
        //if (farm.Theme == FarmThemeList.Country)
        //{
        //    amounts.Add(CountryItemList.GranolaBar, 2);
        //    amounts.Add(CountryItemList.Blanket, 2);
        //}
        return new()
        {
            Farm = farm,
            List = amounts
        };
    }

}