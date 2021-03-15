using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    [SerializeField] private int _health = 10;
    
    public int GetHealth()
    {
        return _health;
    }

    public void SetHealth(int damage)
    {
        _health -= damage;
    }
}
