namespace Phase05MVP4.Components.Custom;

public partial class BasicProgressModal
{
    [Parameter]
    public bool Visible { get; set; }
    [Parameter]
    public EventCallback<bool> VisibleChanged { get; set; }
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }



}