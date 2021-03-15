using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyFactory
{
    void LoadEnemy();
    GameObject CreateEnemy(UnitType enemyType, Vector3 at);
}
