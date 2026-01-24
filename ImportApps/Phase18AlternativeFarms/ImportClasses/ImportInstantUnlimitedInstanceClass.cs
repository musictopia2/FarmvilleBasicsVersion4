namespace Phase18AlternativeFarms.ImportClasses;
public static class ImportInstantUnlimitedInstanceClass
{
    //private static AnimalProgressionPlanDatabase _animalProgression = null!;
    private static CatalogOfferDatabase _catalogOfferDatabase = null!;
    //this time, don't even need level.
    //private static ProgressionProfileDatabase _levelProfile = null!;
    //private static BasicList<AnimalRecipeDocument> _recipes = [];
    public static async Task ImportInstantUnlimitedAsync()
    {
        _catalogOfferDatabase = new();
        BasicList<InstantUnlimitedInstanceDocument> list = [];
        var firsts = FarmHelperClass.GetAllFarms();
        foreach (var item in firsts)
        {
            list.Add(await CreateInstanceAsync(item));
        }
        InstantUnlimitedInstanceDatabase db = new();
        await db.ImportAsync(list);
    }
    private static async Task<InstantUnlimitedInstanceDocument> CreateInstanceAsync(FarmKey farm)
    {
        BasicList<UnlockModel> list = [];
        var plan = await _catalogOfferDatabase.GetCatalogAsync(farm, EnumCatalogCategory.InstantUnlimited);
        foreach (var item in plan)
        {
            bool unlocked = true;
            if (item.Costs.Count > 0)
            {
                unlocked = false; //you have to pay for it first.
            }
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