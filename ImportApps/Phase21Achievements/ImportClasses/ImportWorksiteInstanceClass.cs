namespace Phase21Achievements.ImportClasses;
public static class ImportWorksiteInstanceClass
{
    private static CatalogOfferDatabase _catalogOfferDatabase = null!;
    private static ProgressionProfileDatabase _levelProfile = null!;
    public static async Task ImportWorksitesAsync()
    {
        _catalogOfferDatabase = new();
        _levelProfile = new();
        BasicList<WorksiteInstanceDocument> list = [];
        var farms = FarmHelperClass.GetAllBaselineFarms();
        foreach (var farm in farms)
        {
            list.Add(await CreateBaseInstanceAsync(farm));
        }

        farms = FarmHelperClass.GetAllCoinFarms();
        foreach (var farm in farms)
        {
            list.Add(await CreateCoinInstanceAsync(farm));
        }


        WorksiteInstanceDatabase db = new();
        await db.ImportAsync(list);
    }
    private static async Task<WorksiteInstanceDocument> CreateCoinInstanceAsync(FarmKey farm)
    {
        BasicList<WorksiteAutoResumeModel> worksites = [];

        WorksiteRecipeDatabase recipeDb = new();
        var recipes = await recipeDb.GetRecipesAsync();

        var locations = recipes
            .Where(r => r.Theme == farm.Theme)
            .Select(r => r.Location)
            .Distinct()
            .ToBasicList();

        if (locations.Count == 0)
        {
            throw new CustomBasicException(
                $"No worksite locations found for Theme='{farm.Theme}' ProfileId='{farm.ProfileId}'.");
        }

        foreach (var location in locations)
        {
            worksites.Add(new WorksiteAutoResumeModel
            {
                Name = location
            });
        }


        return new WorksiteInstanceDocument
        {
            Farm = farm,
            Worksites = worksites
        };
    }
    private static async Task<WorksiteInstanceDocument> CreateBaseInstanceAsync(FarmKey farm)
    {
        BasicList<WorksiteAutoResumeModel> worksites = [];
        var plan = await _catalogOfferDatabase.GetCatalogAsync(farm, EnumCatalogCategory.Worksite);
        var profile = await _levelProfile.GetProfileAsync(farm);
        int level = profile.Level;
        foreach (var item in plan)
        {
            bool unlocked = level >= item.LevelRequired;
            worksites.Add(new()
            {
                Name = item.TargetName,
                Unlocked = unlocked
            });
        }
        return new WorksiteInstanceDocument
        {
            Farm = farm,
            Worksites = worksites
        };
    }
}