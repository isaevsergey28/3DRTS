using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UnitFactory : IUnitFactory
{

    private object _meleeUnitPrefab;
    private object _rangedUnitPrefab;

    private GameObject _unitsParent;
    private DiContainer _diContainer;

    public UnitFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
        _unitsParent = GameObject.Find("Units");
    }

    public GameObject CreateUnit(UnitType unitType, Vector3 at)
    {
        GameObject unit = null;
        switch (unitType)
        {
            case UnitType.Melee:
                unit = _diContainer.InstantiatePrefab((GameObject)_meleeUnitPrefab, at, Quaternion.identity, _unitsParent.transform);
                break;
            case UnitType.Ranged:
                unit = _diContainer.InstantiatePrefab((GameObject)_rangedUnitPrefab, at, Quaternion.identity, _unitsParent.transform);
                break;
        }
        return unit;
    }

    public void LoadUnit()
    {
        _meleeUnitPrefab = Resources.Load("MeleeUnit");
        _rangedUnitPrefab = Resources.Load("RangedUnit");
    }
   
}
