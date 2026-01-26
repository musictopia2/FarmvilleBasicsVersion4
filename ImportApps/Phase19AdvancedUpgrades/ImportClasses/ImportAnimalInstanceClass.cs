namespace Phase19AdvancedUpgrades.ImportClasses;
public static class ImportAnimalInstanceClass
{
    private static AnimalProgressionPlanDatabase _animalProgression = null!;
    private static CatalogOfferDatabase _catalogOfferDatabase = null!;
    private static ProgressionProfileDatabase _levelProfile = null!;
    private static BasicList<AnimalRecipeDocument> _recipes = [];
    public static async Task ImportAnimalsAsync()
    {
        _animalProgression = new();
        _catalogOfferDatabase = new();
        _levelProfile = new();

        AnimalRecipeDatabase recipeDb = new();
        _recipes = await recipeDb.GetRecipesAsync();
        if (_recipes.Count == 0)
        {
            throw new CustomBasicException("No animal recipes were imported.");
        }

        BasicList<AnimalInstanceDocument> docs = [];

        // Main
        foreach (var farm in FarmHelperClass.GetAllBaselineFarms())
        {
            docs.Add(await CreateBaselineInstanceAsync(farm));
        }

        // Alternative
        foreach (var farm in FarmHelperClass.GetAllCoinFarms())
        {
            docs.Add(await CreateCoinInstanceAsync(farm, animalsPerRecipe: 3));
        }

        AnimalInstanceDatabase db = new();
        await db.ImportAsync(docs);
    }
    private static async Task<AnimalInstanceDocument> CreateCoinInstanceAsync(FarmKey farm, int animalsPerRecipe)
    {
        // If alt farm truly has no store, don’t touch catalog/level here.
        InstantUnlimitedInstanceDatabase unlimitedDb = new();
        var unlockedUnlimited = await unlimitedDb.GetUnlockedItems(farm);

        // Theme-scoped recipes (important!)
        var recipesForTheme = _recipes
            .Where(r => r.Theme == farm.Theme)
            .ToBasicList();

        if (recipesForTheme.Count == 0)
        {
            throw new CustomBasicException($"No animal recipes found for Theme='{farm.Theme}'.");
        }

        BasicList<AnimalAutoResumeModel> animals = [];


        foreach (var recipe in recipesForTheme)
        {
            if (recipe.Options.Count == 0)
            {
                throw new CustomBasicException(
                    $"Animal recipe '{recipe.Animal}' has no production options.");
            }

            var firstOption = recipe.Options[0];

            // If output is instant-unlimited, skip this animal entirely
            if (unlockedUnlimited.Any(x => x.Name == firstOption.Output.Item))
            {
                continue;
            }

            animalsPerRecipe.Times(_ =>
            {
                animals.Add(new AnimalAutoResumeModel
                {
                    Name = recipe.Animal,
                    Unlocked = true,
                    State = EnumAnimalState.Collecting,
                    ProductionOptionsAllowed = recipe.Options.Count,
                    OutputReady = firstOption.Output.Amount
                });
            });
        }

        return new AnimalInstanceDocument
        {
            Farm = farm,
            Animals = animals
        };
    }


    private static async Task<AnimalInstanceDocument> CreateBaselineInstanceAsync(FarmKey farm)
    {
        InstantUnlimitedInstanceDatabase db = new();
        var list = await db.GetUnlockedItems(farm);
        BasicList<AnimalAutoResumeModel> animals = [];
        var animalPlan = await _animalProgression.GetPlanAsync(farm);
        var current = await _catalogOfferDatabase.GetCatalogAsync(farm, EnumCatalogCategory.Animal);
        var profile = await _levelProfile.GetProfileAsync(farm);
        int level = profile.Level;
        foreach (var item in current)
        {
            bool unlocked = level >= item.LevelRequired;
            if (item.Costs.Count > 0)
            {
                unlocked = false; //you have to pay for it first.
            }
            var productionOptionsAllowed = animalPlan.UnlockRules.Count(x => x.LevelRequired <= level && x.ItemName == item.TargetName);
            productionOptionsAllowed += 1;
            EnumAnimalState state = EnumAnimalState.None;
            int ready = 0;
            var recipe = _recipes.Single(x => x.Animal == item.TargetName);
            bool suppressed = false;
            if (list.Any(x => x.Name == recipe.Options.First().Output.Item))
            {
                suppressed = true;
            }

            if (unlocked)
            {
                
                state = EnumAnimalState.Collecting;
                
                ready = recipe.Options.First().Output.Amount;
            }
            animals.Add(new AnimalAutoResumeModel
            {
                Name = item.TargetName,
                Unlocked = unlocked,
                IsSuppressed = suppressed,
                State = state,
                ProductionOptionsAllowed = productionOptionsAllowed,
                OutputReady = ready
            });
        }
        return new AnimalInstanceDocument
        {
            Farm = farm,
            Animals = animals
        };
    }
}