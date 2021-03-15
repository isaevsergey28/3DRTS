using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private UnitSpawner _unitSpawnerObject;
    [SerializeField] private ActiveUnits _activeUnits;
    [SerializeField] private ActiveEnemies _activeEnemies;
    [SerializeField] private Camera _mainCamera;

    public override void InstallBindings()
    {
        BindUnitFactory();
        BindSpawnButton();
        BindBuildingFactory();
        BindEnemyFactory();
        BindAllActiveUnitsWithEnemies();
        BindAllActiveEnemiesWithUnits();
        BindCamera();
    }

    private void BindCamera()
    {
        Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();
    }

    private void BindAllActiveEnemiesWithUnits()
    {
        Container.Bind<ActiveEnemies>().FromInstance(_activeEnemies).AsSingle();
    }

    private void BindAllActiveUnitsWithEnemies()
    {
        Container.Bind<ActiveUnits>().FromInstance(_activeUnits).AsSingle();
    }

    private void BindEnemyFactory()
    {
        Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
    }

    private void BindBuildingFactory()
    {
        Container.Bind<IBuildingFactory>().To<BuildingFactory>().AsSingle();
    }

    private void BindSpawnButton()
    {
        Container.Bind<UnitSpawner>().FromInstance(_unitSpawnerObject).AsSingle();
    }

    private void BindUnitFactory()
    {
        Container.Bind<IUnitFactory>().To<UnitFactory>().AsSingle();
    }
}