
namespace Phase05MVP4.DataAccess.Rentals;
public class RentalInstanceDocument : IFarmDocumentModel
{
    public FarmKey Farm { get; set; }
    public BasicList<RentalInstanceModel> Rentals { get; set; } = [];
}