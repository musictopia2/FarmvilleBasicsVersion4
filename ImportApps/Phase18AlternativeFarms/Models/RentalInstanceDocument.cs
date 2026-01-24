namespace Phase18AlternativeFarms.Models;
public class RentalInstanceDocument : IFarmDocument
{
    public FarmKey Farm { get; set; }
    public BasicList<RentalInstanceModel> Rentals { get; set; } = [];
}