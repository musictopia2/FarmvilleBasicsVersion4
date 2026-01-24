namespace Phase18AlternativeFarms.ImportClasses;
public static class ImportRentalsClass
{
    public static async Task ImportRentalsAsync()
    {
        var farms = FarmHelperClass.GetAllCompleteFarms(); //i think this can do it to prevent errors.
        BasicList<RentalInstanceDocument> list = [];
        foreach (var farm in farms)
        {
            RentalInstanceDocument rental = new()
            {
                Farm = farm,
                Rentals = []
            };
            list.Add(rental);
        }
        RentalInstanceDatabase db = new();
        await db.ImportAsync(list);
    }
}