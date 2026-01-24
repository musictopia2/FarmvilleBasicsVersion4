
namespace Phase01AlternativeFarms.Components.Custom;
public partial class SpeedSeedComponent(IToast toast)
{
    //for quite a while, a person has to do one at a time.
    //if i do allow a custom amount (that is later).

    private BasicList<GrantableItem> _list = [];

    private bool _showAnimals;


    private void ShowAnimals()
    {
        _showAnimals = true;
    }
    protected override void OnInitialized()
    {
        _list.AddRange(TreeManager.GetUnlockedTreeGrantItems());
        _list.AddRange(CropManager.GetUnlockedCropGrantItems());
        //doing trees first.
        base.OnInitialized();
    }
    private static string DisplayAmount(GrantableItem item) => $"+ {item.Amount}";
    private void TryToUse(GrantableItem item)
    {
        if (InventoryManager.CanAdd(item) == false)
        {
            
            toast.ShowUserErrorToast($"Silot is full. Unable to add {item.Item}");
            return;
        }
        if (InventoryManager.Has(CurrencyKeys.SpeedSeed, 1) == false)
        {
            toast.ShowUserErrorToast("You have no more speed seeds left");
            return;
        }

        if (item.Category == EnumItemCategory.Tree)
        {
            TreeManager.GrantTreeItems(item, 1);
            return;
        }
        if (item.Category == EnumItemCategory.Crop)
        {
            CropManager.GrantCropItems(item, 1);
            return;
        }
        toast.ShowWarningToast("Not supported yet");

        //toast.ShowInfoToast("Trying to use speed seed");
    }


}