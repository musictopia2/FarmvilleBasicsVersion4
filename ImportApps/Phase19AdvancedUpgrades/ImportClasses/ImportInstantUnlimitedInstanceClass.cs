namespace Phase19AdvancedUpgrades.ImportClasses;
public static class ImportInstantUnlimitedInstanceClass
{
    private static CatalogOfferDatabase _catalogOfferDatabase = null!;
    public static async Task ImportInstantUnlimitedAsync()
    {
        _catalogOfferDatabase = new();
        BasicList<InstantUnlimitedInstanceDocument> list = [];
        var firsts = FarmHelperClass.GetAllBaselineFarms();
        foreach (var item in firsts)
        {
            list.Add(await CreateBaselineInstanceAsync(item));
        }
        firsts = FarmHelperClass.GetAllCoinFarms();
        foreach (var item in firsts)
        {
            if (item.Theme == FarmThemeList.Tropical)
            {
                list.Add(CreateTropicalCoinInstances(item));
            }
            else if (item.Theme == FarmThemeList.Country)
            {
                list.Add(CreateCountryCoinInstances(item));
            }
            else
            {
                throw new CustomBasicException("Not Supported");
            }
        }
        InstantUnlimitedInstanceDatabase db = new();
        await db.ImportAsync(list);
    }
    private static InstantUnlimitedInstanceDocument CreateTropicalCoinInstances(FarmKey farm)
    {
        BasicList<string> names =
            [
                TropicalItemList.Fish,
                TropicalItemList.Pineapple,
                TropicalItemList.Coconut
            ];
        InstantUnlimitedInstanceDocument doc = new()
        {
            Farm = farm
        };
        doc.Items.UnlockSeveralItems(names);
        return doc;
    }
    private static InstantUnlimitedInstanceDocument CreateCountryCoinInstances(FarmKey farm)
    {
        BasicList<string> names =
            [
                CountryItemList.Milk,
                CountryItemList.Wheat,
                CountryItemList.Apple
            ];
        InstantUnlimitedInstanceDocument doc = new()
        {
            Farm = farm
        };
        doc.Items.UnlockSeveralItems(names);
        return doc;
    }
    private static async Task<InstantUnlimitedInstanceDocument> CreateBaselineInstanceAsync(FarmKey farm)
    {
        BasicList<UnlockModel> list = [];
        var offers = await _catalogOfferDatabase.GetCatalogAsync(farm, EnumCatalogCategory.InstantUnlimited);
        foreach (var item in offers)
        {
            bool unlocked = item.Costs.Count == 0;
            list.Add(new()
            {
                Name = item.TargetName,
                Unlocked = unlocked
            });
        }
        return new InstantUnlimitedInstanceDocument
        {
            Farm = farm,
            Items = list
        };
    }
}