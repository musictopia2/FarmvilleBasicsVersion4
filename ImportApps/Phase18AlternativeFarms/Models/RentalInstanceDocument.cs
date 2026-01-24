namespace Phase18AlternativeFarms.Models;
public class RentalInstanceDocument : IFarmDocumentModel
{
    public FarmKey Farm { get; set; }
    public BasicList<RentalInstanceModel> Rentals { get; set; } = [];
}