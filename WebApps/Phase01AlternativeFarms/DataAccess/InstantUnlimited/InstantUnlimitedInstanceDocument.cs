namespace Phase01AlternativeFarms.DataAccess.InstantUnlimited;
public class InstantUnlimitedInstanceDocument : IFarmDocumentModel
{
    public required FarmKey Farm { get; set; }
    public BasicList<UnlockModel> Items { get; set; } = [];
}