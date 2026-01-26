using Phase02AdvancedUpgrades.Components.Custom; //for now.
using Phase02AdvancedUpgrades.DataAccess.Animals;
using Phase02AdvancedUpgrades.DataAccess.Balance;
using Phase02AdvancedUpgrades.DataAccess.Catalog;
using Phase02AdvancedUpgrades.DataAccess.Core;
using Phase02AdvancedUpgrades.DataAccess.Crops;
using Phase02AdvancedUpgrades.DataAccess.InstantUnlimited;
using Phase02AdvancedUpgrades.DataAccess.Items;
using Phase02AdvancedUpgrades.DataAccess.OutputAugmentation;
using Phase02AdvancedUpgrades.DataAccess.Progression;
using Phase02AdvancedUpgrades.DataAccess.Quests; //not common enough.
using Phase02AdvancedUpgrades.DataAccess.Rentals;
using Phase02AdvancedUpgrades.DataAccess.Scenarios;
using Phase02AdvancedUpgrades.DataAccess.Store;
using Phase02AdvancedUpgrades.DataAccess.TimedBoosts;
using Phase02AdvancedUpgrades.DataAccess.Trees;
using Phase02AdvancedUpgrades.DataAccess.Upgrades;
using Phase02AdvancedUpgrades.DataAccess.Workers;
using Phase02AdvancedUpgrades.DataAccess.Workshops;
using Phase02AdvancedUpgrades.DataAccess.Worksites;
//these was not common enough to put into global usings.

namespace Phase02AdvancedUpgrades;

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