namespace Phase02AdvancedUpgrades.Services.Core;
public class GameTimerService(IStartFarmRegistry farmRegistry,
    IInventoryRepository inventoryRepository,
    IBaseBalanceProvider baseBalanceProvider,
    GameRegistry gameRegistry, IServiceProvider sp) : BackgroundService
{
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        BasicList<FarmKey> firsts = await farmRegistry.GetFarmsAsync();
        foreach (var farm in firsts)
        {
            //could create a factory to produce this.
            //here will need to figure out the interface for this.

            ItemRegistry itemRegistry = new();
            InventoryManager inventory = new(farm, inventoryRepository, itemRegistry);
            IInventoryFactory starts = sp.GetRequiredService<IInventoryFactory>();
            ICropFactory cropFactory = sp.GetRequiredService<ICropFactory>();
            ITreeFactory treeFactory = sp.GetRequiredService<ITreeFactory>();
            IAnimalFactory animalFactory = sp.GetRequiredService<IAnimalFactory>();
            IWorkshopFactory workshopFactory = sp.GetRequiredService<IWorkshopFactory>();
            IWorksiteFactory worksiteFactory = sp.GetRequiredService<IWorksiteFactory>();
            IWorkerFactory workerFactory = sp.GetRequiredService<IWorkerFactory>();
            IQuestFactory questFactory = sp.GetRequiredService<IQuestFactory>();
            IScenarioFactory scenarioFactory = sp.GetRequiredService<IScenarioFactory>();
            IUpgradeFactory upgradeFactory = sp.GetRequiredService<IUpgradeFactory>();
            IProgressionFactory progressionFactory = sp.GetRequiredService<IProgressionFactory>();
            ICatalogFactory catalogFactory = sp.GetRequiredService<ICatalogFactory>();
            IStoreFactory storeFactory = sp.GetRequiredService<IStoreFactory>();
            IItemFactory itemFactory = sp.GetRequiredService<IItemFactory>();
            IInstantUnlimitedFactory instantUnlimitedFactory = sp.GetRequiredService<IInstantUnlimitedFactory>();
            ITimedBoostFactory timedBoostFactory = sp.GetRequiredService<ITimedBoostFactory>();
            IOutputAugmentationFactory outputAugmentationFactory = sp.GetRequiredService<IOutputAugmentationFactory>();
            IRentalFactory rentalFactory = sp.GetRequiredService<IRentalFactory>();
            TimedBoostManager timedBoostManager = new();
            OutputAugmentationManager outputAugmentationManager = new();
            CropManager cropManager = new(inventory, baseBalanceProvider, itemRegistry, timedBoostManager, outputAugmentationManager);
            TreeManager treeManager = new(inventory, baseBalanceProvider, itemRegistry, timedBoostManager, outputAugmentationManager);
            AnimalManager animalManager = new(inventory, baseBalanceProvider, itemRegistry, timedBoostManager, outputAugmentationManager);
            WorkshopManager workshopManager = new(inventory, baseBalanceProvider, itemRegistry, timedBoostManager, outputAugmentationManager);
            WorksiteManager worksiteManager = new(inventory, baseBalanceProvider, itemRegistry, timedBoostManager, outputAugmentationManager);
            ItemManager itemManager = new();
            CatalogManager catalogManager = new();
            InstantUnlimitedManager instantUnlimitedManager = new(cropManager, treeManager, animalManager, inventory, itemManager);
            RentalManager rentalManager = new(treeManager, animalManager, workshopManager, worksiteManager, instantUnlimitedManager);
            var profile = starts.GetInventoryProfile(farm);
            UpgradeManager upgradeManager = new(inventory, profile, cropManager, animalManager, treeManager, workshopManager);
            ProgressionManager progressionManager = new(inventory, cropManager,
                animalManager, treeManager, workshopManager, worksiteManager, catalogManager);
            StoreManager storeManager = new(progressionManager, treeManager,
                animalManager, workshopManager, worksiteManager,
                catalogManager, inventory, instantUnlimitedManager, timedBoostManager, rentalManager
                );
            QuestManager questManager = new(inventory, itemManager, progressionManager);
            ScenarioManager scenarioManager = new(inventory, cropManager, treeManager, animalManager, workshopManager, worksiteManager);
            IGameTimer timer = new BasicGameState(
                inventory, starts,
                cropFactory, treeFactory, animalFactory, workshopFactory,
                worksiteFactory, workerFactory, questFactory,
                upgradeFactory, progressionFactory, catalogFactory, 
                storeFactory, itemFactory, instantUnlimitedFactory, 
                timedBoostFactory, outputAugmentationFactory, rentalFactory, scenarioFactory,
                cropManager, treeManager, animalManager,
                workshopManager, worksiteManager, questManager,
                upgradeManager, progressionManager, catalogManager, 
                storeManager, itemManager, instantUnlimitedManager, 
                timedBoostManager, outputAugmentationManager, rentalManager, scenarioManager, farm
                );
            await gameRegistry.InitializeFarmAsync(timer, farm);
        }
        await base.StartAsync(cancellationToken);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await gameRegistry.TickAsync();
            }
            catch
            {
                // ignore
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}