
namespace Phase01AlternativeFarms.DataAccess.Rentals;
public class RentalInstanceDocument : IFarmDocument
{
    public FarmKey Farm { get; set; }
    public BasicList<RentalInstanceModel> Rentals { get; set; } = [];
}