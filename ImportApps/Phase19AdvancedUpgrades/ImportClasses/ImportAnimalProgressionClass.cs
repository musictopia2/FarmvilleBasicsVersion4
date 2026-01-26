namespace Phase19AdvancedUpgrades.ImportClasses;
public static class ImportAnimalProgressionClass
{
    public static async Task ImportAnimalsAsync()
    {
        var firsts = FarmHelperClass.GetAllCompleteFarms();
        BasicList<AnimalProgressionPlanDocument> list = [];
        foreach (var item in firsts)
        {
            list.Add(GeneratePlanFarm(item));
        }
        AnimalProgressionPlanDatabase db = new();
        await db.ImportAsync(list);
    }
    private static AnimalProgressionPlanDocument GeneratePlanFarm(FarmKey farm)
    {
        AnimalProgressionPlanDocument document = new()
        {
            Farm = farm
        };
        if (farm.Theme == FarmThemeList.Tropical)
        {
            document.UnlockRules = GetUnlockRulesForTropical();
        }
        else if (farm.Theme == FarmThemeList.Country)
        {
            document.UnlockRules = GetUnlockRulesForCountry();
        }
        else
        {
            throw new CustomBasicException("Not supported");
        }
        return document;
    }
    //still need the progression because it shows how many to unlock of the animal (for what production options you have)
    private static BasicList<ItemUnlockRule> GetUnlockRulesForTropical()
    {
        BasicList<ItemUnlockRule> output = [];
        //the first option is not needed because it comes from the catalog.

        //output.Add(new()
        //{
        //    ItemName = TropicalAnimalListClass.Dolphin,
        //    LevelRequired = 2
        //});
        output.Add(new()
        {
            ItemName = TropicalAnimalListClass.Dolphin,
            LevelRequired = 3
        });
        output.Add(new()
        {
            ItemName = TropicalAnimalListClass.Dolphin,
            LevelRequired = 4
        });
        //output.Add(new()
        //{
        //    ItemName = TropicalAnimalListClass.Chicken,
        //    LevelRequired = 4
        //});
        output.Add(new()
        {
            ItemName = TropicalAnimalListClass.Chicken,
            LevelRequired = 5
        });
        output.Add(new()
        {
            ItemName = TropicalAnimalListClass.Chicken,
            LevelRequired = 6
        });
        //output.Add(new()
        //{
        //    ItemName = TropicalAnimalListClass.Boar,
        //    LevelRequired = 11
        //});
        output.Add(new()
        {
            ItemName = TropicalAnimalListClass.Boar,
            LevelRequired = 12
        });
        output.Add(new()
        {
            ItemName = TropicalAnimalListClass.Boar,
            LevelRequired = 13
        });
        return output;
    }
    private static BasicList<ItemUnlockRule> GetUnlockRulesForCountry()
    {
        BasicList<ItemUnlockRule> output = [];
        //output.Add(new()
        //{
        //    ItemName = CountryAnimalListClass.Cow,
        //    LevelRequired = 2
        //});
        output.Add(new()
        {
            ItemName = CountryAnimalListClass.Cow,
            LevelRequired = 3
        });
        output.Add(new()
        {
            ItemName = CountryAnimalListClass.Cow,
            LevelRequired = 4
        });

        //output.Add(new()
        //{
        //    ItemName = CountryAnimalListClass.Goat,
        //    LevelRequired = 12
        //});
        output.Add(new()
        {
            ItemName = CountryAnimalListClass.Goat,
            LevelRequired = 13
        });
        output.Add(new()
        {
            ItemName = CountryAnimalListClass.Goat,
            LevelRequired = 14
        });
        //output.Add(new()
        //{
        //    ItemName = CountryAnimalListClass.Sheep,
        //    LevelRequired = 14
        //});
        output.Add(new()
        {
            ItemName = CountryAnimalListClass.Sheep,
            LevelRequired = 15
        });
        output.Add(new()
        {
            ItemName = CountryAnimalListClass.Sheep,
            LevelRequired = 16
        });
        return output;
    }
}