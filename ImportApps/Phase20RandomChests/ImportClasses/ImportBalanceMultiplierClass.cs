namespace Phase20RandomChests.ImportClasses;
public static class ImportBalanceMultiplierClass
{
    public static async Task ImportBalanceMultiplierAsync()
    {
        BasicList<BalanceProfileDocument> list = [];

        // MVP1 Production:
        // - All activities take 50% of their base recipe time

        BasicList<FarmKey> firsts = FarmHelperClass.GetAllCompleteFarms();

        foreach (FarmKey key in firsts)
        {
            var multiplier = key.IsBaseline ? 0.5 : 0.01;
            list.Add(CreateFarm(key, multiplier));
        }


        list.ForEach(Validate);
        BalanceProfileDatabase db = new();
        await db.ImportAsync(list);
    }

    

    private static void Validate(BalanceProfileDocument b)
    {
        ValidateOne(b.CropTimeMultiplier, nameof(b.CropTimeMultiplier), b.Farm);
        ValidateOne(b.AnimalTimeMultiplier, nameof(b.AnimalTimeMultiplier), b.Farm);
        ValidateOne(b.WorkshopTimeMultiplier, nameof(b.WorkshopTimeMultiplier), b.Farm);
        ValidateOne(b.TreeTimeMultiplier, nameof(b.TreeTimeMultiplier), b.Farm);
        ValidateOne(b.WorksiteTimeMultiplier, nameof(b.WorksiteTimeMultiplier), b.Farm);
    }
    private static void ValidateOne(double value, string name, FarmKey farm)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
        {
            throw new CustomBasicException($"Invalid {name}='{value}' for Farm='{farm}'. Must be finite.");
        }

        if (value <= 0)
        {
            throw new CustomBasicException($"Invalid {name}='{value}' for Farm='{farm}'. Must be > 0.");
        }

        if (value > 1.0)
        {
            throw new CustomBasicException(
                $"Invalid {name}='{value}' for Farm='{farm}'. Must be <= 1.0 (game never slower than base).");
        }
    }


    private static BalanceProfileDocument CreateFarm(
        FarmKey farm,
        double baseMultiplier,
        double? cropOverride = null,
        double? animalOverride = null,
        double? workshopOverride = null,
        double? treeOverride = null,
        double? worksiteOverride = null)
    {
        return new BalanceProfileDocument
        {
            Farm = farm,
            CropTimeMultiplier = cropOverride ?? baseMultiplier,
            AnimalTimeMultiplier = animalOverride ?? baseMultiplier,
            WorkshopTimeMultiplier = workshopOverride ?? baseMultiplier,
            TreeTimeMultiplier = treeOverride ?? baseMultiplier,
            WorksiteTimeMultiplier = worksiteOverride ?? baseMultiplier
        };
    }

}
