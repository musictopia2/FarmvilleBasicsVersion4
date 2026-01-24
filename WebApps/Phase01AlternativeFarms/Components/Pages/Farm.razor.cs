using BasicBlazorLibrary.Components.Toasts;
using Phase01AlternativeFarms.Components.Custom;

namespace Phase01AlternativeFarms.Components.Pages;
public partial class Farm(GameRegistry registry, FarmContext context) : IDisposable
{
    [Parameter]
    public string Theme { get; set; } = string.Empty;

    [Parameter]
    public string Player { get; set; } = string.Empty;

    [Parameter]
    public string ProfileId { get; set; } = string.Empty;
    [Parameter] public string SlotText { get; set; } = "";



    private EnumFarmSlot Slot { get; set; } = EnumFarmSlot.Main;

    private MainFarmContainer? _farmContainer;
    protected override void OnParametersSet()
    {
        if (!string.IsNullOrWhiteSpace(SlotText) &&
        Enum.TryParse<EnumFarmSlot>(SlotText, ignoreCase: true, out var parsed))
        {
            Slot = parsed;
        }
        else
        {
            Slot = EnumFarmSlot.Main;
        }
        FarmKey player = new()
        {
            Theme = Theme,
            PlayerName = Player,
            ProfileId = ProfileId,
            Slot = Slot
        };
        _farmContainer = registry.GetFarm(player);
        context.Set(_farmContainer);
        base.OnParametersSet();
    }
    protected override void OnInitialized()
    {
        BlazoredToasts.Timeout = 2; //needs to be less time.
        BlazoredToasts.TopOffset = "250px";
        
        base.OnInitialized();
    }

    public void Dispose()
    {
        BlazoredToasts.TopOffset = "4rem"; //the default.
        GC.SuppressFinalize(this);
    }
}