namespace Phase18AlternativeFarms.ImportClasses;
public static class ImportWorkerInstanceClass
{
    private static CatalogOfferDatabase _catalogOfferDatabase = null!;
    private static ProgressionProfileDatabase _levelProfile = null!;
    public static async Task ImportWorkersAsync()
    {
        _catalogOfferDatabase = new();
        _levelProfile = new();
        BasicList<WorkerInstanceDocument> list = [];
        var farms = FarmHelperClass.GetAllBaselineFarms();
        foreach (var farm in farms)
        {
            list.Add(await CreateBaselineInstanceAsync(farm));
        }
        farms = FarmHelperClass.GetAllCoinFarms();
        foreach (var farm in farms)
        {
            list.Add(await CreateCoinInstanceAsync(farm));
        }
        WorkerInstanceDatabase db = new();
        await db.ImportAsync(list);
    }
    private static async Task<WorkerInstanceDocument> CreateCoinInstanceAsync(FarmKey farm)
    {
        BasicList<UnlockModel> workers = [];


        WorkerRecipeDatabase db = new();
        var list = await db.GetWorkersAsync(farm.Theme);
        foreach(var worker in list)
        {
            workers.Add(new()
            {
                Name = worker.WorkerName,
                Unlocked = true
            });
        }

        return new WorkerInstanceDocument
        {
            Farm = farm,
            Workers = workers
        };
    }
    private static async Task<WorkerInstanceDocument> CreateBaselineInstanceAsync(FarmKey farm)
    {
        BasicList<UnlockModel> workers = [];
        var plan = await _catalogOfferDatabase.GetCatalogAsync(farm, EnumCatalogCategory.Worker);
        var profile = await _levelProfile.GetProfileAsync(farm);
        int level = profile.Level;
        foreach (var item in plan)
        {
            bool unlocked = level >= item.LevelRequired && item.Costs.Count == 0;
            workers.Add(new()
            {
                Name = item.TargetName,
                Unlocked = unlocked
            });
        }
        return new WorkerInstanceDocument
        {
            Farm = farm,
            Workers = workers
        };
    }

}
