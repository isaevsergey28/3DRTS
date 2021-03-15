using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortressInfo : MonoBehaviour
{
    [SerializeField] private int _fortressHealth = 100;

    [SerializeField] private GameObject _explosionPrefab;


    [SerializeField] private GameObject _loseText;
    public int GetHealth()
    {
        return _fortressHealth;
    }
    public void SetFortressHealth(int damage)
    {
        _fortressHealth -= damage;
    }

    public void CreateExplosion()
    {
        GameObject explosion;
        Destroy(this.gameObject.transform.GetChild(0).gameObject);
        explosion = Instantiate(_explosionPrefab, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity, transform);
        _loseText.SetActive(true);
    }
}
