namespace Phase19AdvancedUpgrades.DataAccess;
public static class ImportCropInstanceClass
{
    private static CropProgressionPlanDatabase _cropProgression = null!;
    private static ProgressionProfileDatabase _levelProfile = null!;
    public static async Task ImportCropsAsync()
    {
        BasicList<CropInstanceDocument> list = [];
        _cropProgression = new();
        _levelProfile = new();
        var firsts = FarmHelperClass.GetAllBaselineFarms();
        foreach (var farm in firsts)
        {
            list.Add(await CreateBaselineInstanceAsync(farm));
        }
        firsts = FarmHelperClass.GetAllCoinFarms();
        CropRecipeDatabase recipeDb = new();
        var recipes = await recipeDb.GetRecipesAsync();
        foreach (var farm in firsts)
        {
            list.Add(await CreateCoinInstancesAsync(farm, recipes));
        }
        CropInstanceDatabase db = new();
        await db.ImportAsync(list);
    }
    private static async Task<CropInstanceDocument> CreateCoinInstancesAsync(FarmKey farm, BasicList<CropRecipeDocument> recipes)
    {
        BasicList<CropAutoResumeModel> slots = [];
        BasicList<CropDataModel> crops = [];
        InstantUnlimitedInstanceDatabase db = new();
        var list = await db.GetUnlockedItems(farm);
        35.Times(_ => slots.Add(new CropAutoResumeModel()));
        recipes.ForConditionalItems(x => x.Theme == farm.Theme, recipe =>
        {
            if (list.Any(x => x.Name == recipe.Item) == false)
            {
                crops.Add(new()
                {
                    Unlocked = true,
                    Item = recipe.Item,
                    IsSuppressed = false
                });
            }
        });
        CropInstanceDocument output = new()
        {
            Farm = farm,
            Crops = crops,
            Slots = slots

        };
        return output;
    }

    private static async Task<CropInstanceDocument> CreateBaselineInstanceAsync(FarmKey farm)
    {
        BasicList<CropAutoResumeModel> slots = [];
        BasicList<CropDataModel> crops = [];
        var cropPlan = await _cropProgression.GetPlanAsync(farm);

        var profile = await _levelProfile.GetProfileAsync(farm);

        InstantUnlimitedInstanceDatabase db = new();
        var list = await db.GetUnlockedItems(farm);
        foreach (var level in cropPlan.SlotLevelRequired)
        {
            bool unlocked = true;
            if (level > profile.Level)
            {
                unlocked = false;
            }
            CropAutoResumeModel slot = new()
            {
                Unlocked = unlocked,
            };
            slots.Add(slot);
        }
        foreach (var item in cropPlan.UnlockRules)
        {
            bool unlocked = true;
            bool supressed = false;
            if (item.LevelRequired > profile.Level)
            {
                unlocked = false;
            }
            if (list.Any(x => x.Name == item.ItemName))
            {
                supressed = true;
            }
            CropDataModel crop = new()
            {
                Item = item.ItemName,
                Unlocked = unlocked,
                IsSuppressed = supressed
            };
            crops.Add(crop);
        }

        CropInstanceDocument output = new()
        {
            Farm = farm,
            Crops = crops,
            Slots = slots

        };
        return output;
    }
}