using Phase01AlternativeFarms.Components.Custom; //for now.
using Phase01AlternativeFarms.DataAccess.Animals;
using Phase01AlternativeFarms.DataAccess.Balance;
using Phase01AlternativeFarms.DataAccess.Catalog;
using Phase01AlternativeFarms.DataAccess.Core;
using Phase01AlternativeFarms.DataAccess.Crops;
using Phase01AlternativeFarms.DataAccess.InstantUnlimited;
using Phase01AlternativeFarms.DataAccess.Items;
using Phase01AlternativeFarms.DataAccess.OutputAugmentation;
using Phase01AlternativeFarms.DataAccess.Progression;
using Phase01AlternativeFarms.DataAccess.Quests; //not common enough.
using Phase01AlternativeFarms.DataAccess.Rentals;
using Phase01AlternativeFarms.DataAccess.Store;
using Phase01AlternativeFarms.DataAccess.TimedBoosts;
using Phase01AlternativeFarms.DataAccess.Trees;
using Phase01AlternativeFarms.DataAccess.Upgrades;
using Phase01AlternativeFarms.DataAccess.Workers;
using Phase01AlternativeFarms.DataAccess.Workshops;
using Phase01AlternativeFarms.DataAccess.Worksites;
//these was not common enough to put into global usings.

namespace Phase01AlternativeFarms;

public static class ServiceExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection RegisterFarmServices()
        {
            services.AddHostedService<GameTimerService>()
                .AddSingleton<GameRegistry>()
                .AddSingleton<IInventoryRepository, InventoryStockDatabase>()
                .AddSingleton<IStartFarmRegistry, StartFarmDatabase>()
                .AddSingleton<IInventoryFactory, InventoryFactory>()
                .AddSingleton<ITreeFactory, TreeFactory>()
                .AddSingleton<ICropFactory, CropFactory>()
                .AddSingleton<IAnimalFactory, AnimalFactory>()
                .AddSingleton<IWorkshopFactory, WorkshopFactory>()
                .AddSingleton<IWorksiteFactory, WorksiteFactory>()
                .AddSingleton<IWorkerFactory, WorkerFactory>()
                .AddSingleton<IQuestFactory, QuestFactory>()
                .AddSingleton<IUpgradeFactory, UpgradeFactory>()
                .AddSingleton<IProgressionFactory, ProgressionFactory>()
                .AddSingleton<ICatalogFactory, CatalogFactory>()
                .AddSingleton<IStoreFactory, StoreFactory>()
                .AddSingleton<IItemFactory, ItemFactory>()
                .AddSingleton<IInstantUnlimitedFactory, InstantUnlimitedFactory>()
                .AddSingleton<ITimedBoostFactory, TimedBoostFactory>()
                .AddSingleton<IOutputAugmentationFactory, OutputAugmentationFactory>()
                .AddSingleton<IRentalFactory, RentalFactory>()
                .AddScoped<ReadyStatusService>()
                .AddScoped<OverlayService>()
                .AddScoped<FarmContext>()
                .AddSingleton<IBaseBalanceProvider, BalanceProfileDatabase>() //i think this is safe this time (refer to inventory persistence)
                ;
            return services;
        }
        //not sure about quests.  not until near the end
    }
}