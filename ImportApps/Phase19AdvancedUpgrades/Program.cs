mm1.Prep();
await DataAccess.ImportStartClass.ImportStartAsync(); //done
await DataAccess.ImportCatalogOfferClass.ImportCatalogAsync();
await DataAccess.ImportOutputAugmentationClass.ImportOutputAugmentationAsync();
await DataAccess.ImportInstantUnlimitedInstanceClass.ImportInstantUnlimitedAsync();
await DataAccess.ImportTimedBoostProfileClass.ImportTimedBoostsAsync();
await DataAccess.ImportInventoryStorageUpgradesClass.ImportInventoryStorageUpgradesAsync(); //this must be first now.  like the old recipes.
await DataAccess.ImportInventoryStorageProfileClass.ImportInventoryProfilesAsync();
await DataAccess.ImportLevelProgressionClass.ImportProgressionAsync();
await DataAccess.ImportProgressionProfileClass.ImportProgressionAsync();
await DataAccess.ImportCropRecipeClass.ImportCropsAsync();
await DataAccess.ImportCropProgressionClass.ImportCropsAsync();
await DataAccess.ImportCropInstanceClass.ImportCropsAsync(); //done
await DataAccess.ImportInventoryStockClass.ImportBeginningInventoryAmountsAsync(); //done
await DataAccess.ImportTreeRecipeClass.ImportTreesAsync();
await DataAccess.ImportTreeInstanceClass.ImportTreesAsync(); //done
await DataAccess.ImportAnimalRecipeClass.ImportAnimalsAsync();
await DataAccess.ImportAnimalProgressionClass.ImportAnimalsAsync();
await DataAccess.ImportAnimalInstanceClass.ImportAnimalsAsync(); //done
await DataAccess.ImportWorkshopRecipeClass.ImportWorkshopsAsync();
await DataAccess.ImportWorkshopCapacityUpgradesClass.ImportWorkshopsAsync();
await DataAccess.ImportWorkshopProgressionClass.ImportWorkshopsAsync();
await DataAccess.ImportWorkshopInstanceClass.ImportWorkshopsAsync(); //done
await DataAccess.ImportWorksiteRecipeClass.ImportWorksitesAsync();
await DataAccess.ImportWorksiteInstanceClass.ImportWorksitesAsync(); //done
await DataAccess.ImportWorkerRecipeClass.ImportWorkersAsync(); //done
await DataAccess.ImportWorkerInstanceClass.ImportWorkersAsync();
await DataAccess.ImportBalanceMultiplierClass.ImportBalanceMultiplierAsync(); //done
await DataAccess.ImportStoreUiStateClass.ImportUiStoreStateAsync();
await DataAccess.ImportItemPlanClass.ImportItemsAsync();
await DataAccess.ImportResetQuestsClass.ResetQuestsAsync();
await DataAccess.ImportScenariosClass.ImportScenariosAsync(); //when resetting back to factory, will not reset this one (for the coin farm).
await DataAccess.ImportRentalsClass.ImportRentalsAsync();
//await ii1.ImportQuestInstancesClass.ImportQuestsAsync(); //iffy
Console.WriteLine("Completed");