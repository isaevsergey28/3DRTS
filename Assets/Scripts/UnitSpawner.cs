using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UnitSpawner : MonoBehaviour
{
    private ActiveUnits _activeUnits;

    private IUnitFactory _unitFactory;

    [SerializeField] private GameObject _gameInfoObject;
    private GameInfo _gameInfoScript;

    [Inject]
    private void Construct(IUnitFactory unitFactory)
    {
        _unitFactory = unitFactory;
        _unitFactory.LoadUnit();
    }

    private void Start()
    {
        _gameInfoScript = _gameInfoObject.GetComponent<GameInfo>();
        _activeUnits = GetComponent<ActiveUnits>();
    }
    public void SpawnUnit(UnitType unitType, Vector3 at)
    {
        _activeUnits.AddUnit(_unitFactory.CreateUnit(unitType, at));
       
        switch(unitType)
        {
            case UnitType.Melee:
                _gameInfoScript.meleeCount++;
                break;
            case UnitType.Ranged:
                _gameInfoScript.rangedCount++;
                break;
        }
    }
}
