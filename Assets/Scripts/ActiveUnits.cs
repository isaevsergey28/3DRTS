using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUnits : MonoBehaviour
{
    [SerializeField] private List<GameObject> allUnits;

    public void AddUnit(GameObject unit)
    {
        allUnits.Add(unit);
    }
    public List<GameObject> GetUnits()
    {
        return allUnits;
    }
    public void DeleteUnit(GameObject unit)
    {
        allUnits.Remove(unit);
    }
}
