namespace Phase05MVP4.Components.Custom;

public partial class AnimalSpeedComponent(IToast toast)
{
    private BasicList<AnimalGrantModel> _animals = [];
    protected override void OnInitialized()
    {
        _animals = AnimalManager.GetUnlockedAnimalGrantItems();
        base.OnInitialized();
    }
    // how many of this animal type the player currently owns (unlocked instances)

    // Keep image naming consistent with AnimalChoiceModal
    private static string Normalize(string text)
        => text.Replace(" ", "").Replace("-", "").Replace("_", "").ToLowerInvariant();

    private static string GetItemImage(string itemName)
        => $"/{Normalize(itemName)}.png";

    private void TryToUse(AnimalGrantModel item)
    {
        if (AnimalManager.CanGrantAnimalItems(item, 1) == false)
        {
            toast.ShowUserErrorToast("Unable to use the speed seed.   Either don't have the requirements or the barn is full");
            return;
        }
        

        AnimalManager.GrantAnimalItems(item, 1);

    }
}