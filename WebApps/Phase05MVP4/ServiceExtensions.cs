using Phase05MVP4.Components.Custom; //for now.
using Phase05MVP4.DataAccess.Achievements;
using Phase05MVP4.DataAccess.Animals;
using Phase05MVP4.DataAccess.Balance;
using Phase05MVP4.DataAccess.Catalog;
using Phase05MVP4.DataAccess.Core;
using Phase05MVP4.DataAccess.Crops;
using Phase05MVP4.DataAccess.InstantUnlimited;
using Phase05MVP4.DataAccess.Items;
using Phase05MVP4.DataAccess.OutputAugmentation;
using Phase05MVP4.DataAccess.Progression;
using Phase05MVP4.DataAccess.Quests; //not common enough.
using Phase05MVP4.DataAccess.RandomChests;
using Phase05MVP4.DataAccess.Rentals;
using Phase05MVP4.DataAccess.Scenarios;
using Phase05MVP4.DataAccess.Store;
using Phase05MVP4.DataAccess.TimedBoosts;
using Phase05MVP4.DataAccess.Trees;
using Phase05MVP4.DataAccess.Upgrades;
using Phase05MVP4.DataAccess.Workers;
using Phase05MVP4.DataAccess.Workshops;
using Phase05MVP4.DataAccess.Worksites;
//these was not common enough to put into global usings.

namespace Phase05MVP4;

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
                .AddSingleton<IRandomChestFactory, RandomChestFactory>()
                .AddSingleton<IAchievementFactory, AchievementFactory>()
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