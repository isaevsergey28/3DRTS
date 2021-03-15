using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedUnitBehaviour : UnitBehaviour
{
    [SerializeField] private GameObject _rockPrefab;

    protected override void AttackUnit(GameObject enemyToAttack)
    {
        Vector3 direction = enemyToAttack.transform.position - transform.position;
        if (direction.magnitude > _stopRadiusForMob)
        {
            _animator.SetBool("isWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, enemyToAttack.transform.position, Time.fixedDeltaTime);
        }
        else if (_selectedEnemyInfo.GetHealth() > 0)
        {
            _animator.SetBool("isWalking", false);
            StartCoroutine(GiveDamageForEnemy());
        }
        transform.LookAt(enemyToAttack.transform);
    }
    private IEnumerator GiveDamageForEnemy()
    {
        GameObject rock;
        _isAttackGone = false;
        _animator.SetBool("isRangedFight", true);
        yield return new WaitForSeconds(1.5f);
        _isAttackGone = true;
        if (Random.Range(0, 6) != 1)
        {
            _selectedEnemyInfo.SetHealth(_damage);
            //rock = Instantiate(_rockPrefab, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity, transform);
            //StartCoroutine(MoveRock(rock));
        }
        base.CheckEnemyAlive();
    }
    private IEnumerator MoveRock(GameObject rock)
    {
        rock.transform.position = Vector3.MoveTowards(rock.transform.position, _selectedEnemyInfo.transform.position, Time.fixedDeltaTime);
        yield return new WaitForSeconds(1f);
        Destroy(rock);
    }
}
