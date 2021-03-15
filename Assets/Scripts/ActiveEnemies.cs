using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemies : MonoBehaviour
{
    [SerializeField] private List<GameObject> allEnemies;

    public void AddEnemy(GameObject enemy)
    {
        allEnemies.Add(enemy);
    }
    public List<GameObject> GetEnemies()
    {
        return allEnemies;
    }
    public void DeleteEnemy(GameObject enemy)
    {
        allEnemies.Remove(enemy);
    }
}
