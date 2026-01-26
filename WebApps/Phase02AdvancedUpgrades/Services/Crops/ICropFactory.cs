namespace Phase02AdvancedUpgrades.Services.Crops;
public interface ICropFactory
{
    CropServicesContext GetCropServices(FarmKey farm);
}