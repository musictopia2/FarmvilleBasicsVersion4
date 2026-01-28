using Phase04Achievements.Components.Custom; //for now.
using Phase04Achievements.DataAccess.Animals;
using Phase04Achievements.DataAccess.Balance;
using Phase04Achievements.DataAccess.Catalog;
using Phase04Achievements.DataAccess.Core;
using Phase04Achievements.DataAccess.Crops;
using Phase04Achievements.DataAccess.InstantUnlimited;
using Phase04Achievements.DataAccess.Items;
using Phase04Achievements.DataAccess.OutputAugmentation;
using Phase04Achievements.DataAccess.Progression;
using Phase04Achievements.DataAccess.Quests; //not common enough.
using Phase04Achievements.DataAccess.RandomChests;
using Phase04Achievements.DataAccess.Rentals;
using Phase04Achievements.DataAccess.Scenarios;
using Phase04Achievements.DataAccess.Store;
using Phase04Achievements.DataAccess.TimedBoosts;
using Phase04Achievements.DataAccess.Trees;
using Phase04Achievements.DataAccess.Upgrades;
using Phase04Achievements.DataAccess.Workers;
using Phase04Achievements.DataAccess.Workshops;
using Phase04Achievements.DataAccess.Worksites;
//these was not common enough to put into global usings.

namespace Phase04Achievements;

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