namespace Phase18AlternativeFarms.ImportClasses;
public static class ImportTreeInstanceClass
{
    private static CatalogOfferDatabase _catalogOfferDatabase = null!;
    private static ProgressionProfileDatabase _levelProfile = null!;
    public static async Task ImportTreesAsync()
    {
        _catalogOfferDatabase = new();
        _levelProfile = new();
        BasicList<TreeInstanceDocument> list = [];
        var farms = FarmHelperClass.GetAllFarms();
        foreach ( var farm in farms )
        {
            list.Add(await CreateInstanceAsync(farm));
        }
        TreeInstanceDatabase db = new();
        await db.ImportAsync(list);
    }
    private static async Task<TreeInstanceDocument> CreateInstanceAsync(FarmKey farm)
    {
        InstantUnlimitedInstanceDatabase db = new();
        var list = await db.GetUnlockedItems(farm);
        TreeRecipeDatabase r = new();
        var recipes = await r.GetRecipesAsync();



        BasicList<TreeAutoResumeModel> trees = [];
        var plan = await _catalogOfferDatabase.GetCatalogAsync(farm, EnumCatalogCategory.Tree);
        //var treelPlan = await _treeProgression.GetPlanAsync(farm);
        var profile = await _levelProfile.GetProfileAsync(farm);
        int level = profile.Level;
        bool suppressed = false;
        foreach (var item in plan)
        {
            bool unlocked = level >= item.LevelRequired;
            if (item.Costs.Count > 0)
            {
                unlocked = false; //you have to pay for it first.
            }

            var recipe = recipes.Single(x => x.TreeName == item.TargetName);

            if (list.Any(x => x.Name == recipe.Item))
            {
                suppressed = true;
                //unlocked = false; //must be false because you are receiving from another source.
            }
            trees.Add(new()
            {
                TreeName = item.TargetName,
                Unlocked = unlocked,
                IsSuppressed = suppressed
            });
        }
        return new TreeInstanceDocument
        {
            Farm = farm,
            Trees = trees
        };
    }
}