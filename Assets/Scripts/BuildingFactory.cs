using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BuildingFactory : IBuildingFactory
{
    private object _meleeBuildingPrefab;
    private object _rangedBuildingPrefab;

    private GameObject _buildParent;
    private DiContainer _diContainer;

    public BuildingFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
        _buildParent = GameObject.Find("AllBuildings");
    }

    public GameObject Create(BuildingType buildType, Vector3 at)
    {
        GameObject build = null;
        switch (buildType)
        {
            case BuildingType.MeleeBarrack:
                build = _diContainer.InstantiatePrefab((GameObject)_meleeBuildingPrefab, at, Quaternion.identity, _buildParent.transform);
                break;
            case BuildingType.RangedBarrack:
                build = _diContainer.InstantiatePrefab((GameObject)_rangedBuildingPrefab, at, Quaternion.identity, _buildParent.transform);
                break;
        }
        return build;
    }

    public void Load()
    {
        _meleeBuildingPrefab = Resources.Load("MeleeBarrack");
        _rangedBuildingPrefab = Resources.Load("RangedBarrack");
    }
}
