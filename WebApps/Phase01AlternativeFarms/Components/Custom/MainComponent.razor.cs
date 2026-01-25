using BasicBlazorLibrary.Components.Layouts;
using BasicBlazorLibrary.Components.NavigationMenus;

namespace Phase01AlternativeFarms.Components.Custom;

public partial class MainComponent(NavigationManager nav, OverlayService service,
    IToast toast, IntegerPickerService quantityPickerService,
    IntegerActionPickerService actionPickerService,
    FarmTransferService farmTransfer
    ) : IDisposable
{



    private NavigationBarContainer? _nav;

    private readonly OverlayInsets _overlays = new();

    private void VisibleChanged(bool visible)
    {
        quantityPickerService.UpdateVisibleStatus(visible);
    }
    private bool HasAnyActiveTimedBoosts => TimedBoostManager.GetActiveBoosts.Count > 0;
    private async Task OnValueChanged(int value)
    {
        quantityPickerService.Submit(value);

        await Task.CompletedTask;
    }

    private string ShowAllWorksitesConfirmation =>
        $"Are you sure you want to complete all worksites?  You have {InventoryManager.Get(CurrencyKeys.FinishAllWorksites)} left";

    private string ShowAllWorkshopsConfirmation =>
        $"Are you sure you want to complete all workshops?  You have {InventoryManager.Get(CurrencyKeys.FinishAllWorkshops)} left";


    private bool _showAllWorksites = false;
    private bool _showAllWorkshops = false;

    private async Task TransferCoinsAsync()
    {
        await ScenarioManager.ClaimRewardAsync(nav, Key, farmTransfer);
    }

    //leave it commented out until i prove my new system works.

    //private async Task TransferCoinAsync()
    //{
    //    if (this.IsCoin == false)
    //    {
    //        return;
    //    }
    //    toast.ShowSuccessToast("Sending 1000 coins. Check to make sure its fine");
    //    await Task.Delay(2000);
    //    //for now, forced to do delay since the reload portion would make the toast go away.
    //    //if i did not force reload, then quests gets hosed. (everything else appear okay).
    //    FarmKey main = this.AsMain;
    //    //figure out how to send to this farm.
    //    transferService.AddCoinFromScenarioCompletion(Key, 1000);
    //    nav.NavigateTo(main); //this will transfer back to the main.  if everything works, you will have the 1000 coins applied when you are on there.

    //}

    private void ProcessAllWorksites(bool confirmed)
    {
        _showAllWorksites = false;
        if (confirmed)
        {
            WorksiteManager.CompleteAllJobsImmediately();
            toast.ShowSuccessToast("Completed all jobs from the worksites.  If there was none that was active, went to waste");
        }
    }
    private void ProcessAllWorkshops(bool confirmed)
    {
        _showAllWorkshops = false;
        if (confirmed)
        {
            WorkshopManager.CompleteAllJobsImmediately();
            toast.ShowSuccessToast("Completed all jobs from the workshops.  If there was none that was active, went to waste");
        }
    }

    private void PossibleAllWorksites()
    {
        if (InventoryManager.Has(CurrencyKeys.FinishAllWorksites, 1) == false)
        {
            toast.ShowUserErrorToast("You do not have any Finish All Worksites consumables left.  Try to purchase some");
            return;
        }
        _showAllWorksites = true;

    }
    private void PossibleAllWorkshops()
    {
        if (InventoryManager.Has(CurrencyKeys.FinishAllWorkshops, 1) == false)
        {
            toast.ShowUserErrorToast("You do not have any Finish All Workshops consumables left.  Try to purchase some");
            return;
        }
        _showAllWorkshops = true;
    }


    private void ShowActiveBoosts()
    {
        _showActiveBoosts = true;
    }

    private void Changed()
    {
        InvokeAsync(StateHasChanged);
    }
    protected override void OnAfterRender(bool firstRender)
    {
        if (_nav is not null)
        {
            _overlays.TopPx = _nav.HeightOfHeader + 5;
            _overlays.BottomPx = 10;
            StateHasChanged();
            return;
        }
        base.OnAfterRender(firstRender);
    }

    private bool _loaded = false;

    protected override void OnInitialized()
    {
        if (Key.IsCoin == false)
        {
            _loaded = true;
            //toast.ShowInfoToast("Coin.  Rethink");
        }
        quantityPickerService.StateChanged = Changed;
        TimedBoostManager.Tick += Changed;
        ScenarioManager.OnUpdated += TryUpdateTasks;
        service.Toast = toast;
        base.OnInitialized();
    }

    private void TryUpdateTasks()
    {
        _status = ScenarioManager.GetStatus;
        if (_status == EnumScenarioStatus.Cooldown)
        {
            _nextScenarioStatus = ScenarioManager.TimeLeft();
        }
    }

    private EnumScenarioStatus _status;
    private string _nextScenarioStatus = "";
    protected override async Task OnInitializedAsync()
    {
        await service.CloseAllAsync();
        if (Key.IsCoin)
        {
            await ScenarioManager.LoadAsync();
            TryUpdateTasks();
            _loaded = true;
        }
        await base.OnInitializedAsync();
    }

    private void Travel()
    {
        FarmKey coop = this.AsCooperative;
        nav.NavigateTo(coop);
    }

    private void Home()
    {
        if (this.IsCoin || this.IsCooperative)
        {
            FarmKey main = this.AsMain;
            nav.NavigateTo(main);
            return;
        }
        //has to figure out an easy way eventually to enable the coop (later).
        service.Reset();
        nav.NavigateTo("/");
    }
    private bool _showBarn = false;
    private bool _showSilo = false;
    private bool _showShop = false;
    private bool _showSpeedSeeds = false;
    private bool _showTimedBoosts = false;
    private bool _showActiveBoosts = false;

    private async Task CloseAllPopupsAsync()
    {
        await service.CloseAllAsync();
    }
    private void ShowTimedBoosts()
    {
        _showTimedBoosts = true;
    }
    private void ShowSpeedSeeds()
    {
        _showSpeedSeeds = true;
    }
    private void ShowShop()
    {
        _showShop = true;
    }
    private void ShowSilo()
    {
        _showSilo = true;
    }

    private void ShowBarn()
    {
        _showBarn = true;
    }
    private void CloseBarn()
    {
        _showBarn = false;
    }

    public void Dispose()
    {
        quantityPickerService.StateChanged = null;
        TimedBoostManager.Tick -= Changed;
        ScenarioManager.OnUpdated -= TryUpdateTasks;
        GC.SuppressFinalize(this);
    }

    private string Title
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Key.Theme))
            {
                return "Needs Theme";
            }
            return $"{Key.PlayerName.ToDisplayPlayerName()} {Key.Theme.GetWords} {Key.ProfileId.GetWords}";
        }
    }

    private void ActionVisibleChanged(bool visible)
    {
        actionPickerService.UpdateVisibleStatus(visible);
    }

    private async Task OnActionValueChanged(int value)
    {
        await actionPickerService.SubmitAsync(value);
    }

}