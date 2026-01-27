using Phase03RandomChests.Components.Custom; //for now.
using Phase03RandomChests.DataAccess.Animals;
using Phase03RandomChests.DataAccess.Balance;
using Phase03RandomChests.DataAccess.Catalog;
using Phase03RandomChests.DataAccess.Core;
using Phase03RandomChests.DataAccess.Crops;
using Phase03RandomChests.DataAccess.InstantUnlimited;
using Phase03RandomChests.DataAccess.Items;
using Phase03RandomChests.DataAccess.OutputAugmentation;
using Phase03RandomChests.DataAccess.Progression;
using Phase03RandomChests.DataAccess.Quests; //not common enough.
using Phase03RandomChests.DataAccess.Rentals;
using Phase03RandomChests.DataAccess.Scenarios;
using Phase03RandomChests.DataAccess.Store;
using Phase03RandomChests.DataAccess.TimedBoosts;
using Phase03RandomChests.DataAccess.Trees;
using Phase03RandomChests.DataAccess.Upgrades;
using Phase03RandomChests.DataAccess.Workers;
using Phase03RandomChests.DataAccess.Workshops;
using Phase03RandomChests.DataAccess.Worksites;
//these was not common enough to put into global usings.

namespace Phase03RandomChests;

public static class ServiceExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection RegisterFarmServices()
        {
            services.AddHostedService<GameTimerService>()
                .AddSingleton<GameRegistry>()
                .AddSingleton<FarmTransferService>()
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
                .AddSingleton<IScenarioFactory, ScenarioFactory>()
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