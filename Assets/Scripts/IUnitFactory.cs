using UnityEngine;

public interface IUnitFactory
{
    void LoadUnit();
    GameObject CreateUnit(UnitType enemyType, Vector3 at);
}
