namespace Phase01AlternativeFarms.Components.Custom;
public class FarmContext
{
    public MainFarmContainer? Current { get; private set; }
    public void Set(MainFarmContainer farm) => Current = farm;
}