
namespace Phase01AlternativeFarms.DataAccess.Store;
public class StoreFactory : IStoreFactory
{
    StoreServicesContext IStoreFactory.GetStoreServices(FarmKey farm)
    {
        return new()
        {
            UiStateRepository = new StoreUiStateDatabase(farm)
        };
    }
}