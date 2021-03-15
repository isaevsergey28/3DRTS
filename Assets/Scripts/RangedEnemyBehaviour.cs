using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBehaviour : EnemyBehaviour
{
    [SerializeField] private GameObject _rockPrefab;

    protected override void AttackUnit(GameObject unitToAttack)
   {
        Vector3 direction = unitToAttack.transform.position - transform.position;
        if (direction.magnitude > _stopRadiusForMob)
        {
            _animator.SetBool("isWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, unitToAttack.transform.position, Time.fixedDeltaTime);
        }
        else if(_selectedUnitInfo.GetHealth() > 0)
        {
            _animator.SetBool("isWalking", false);
            StartCoroutine(GiveDamage());
        }
        transform.LookAt(unitToAttack.transform);
    }

    private IEnumerator GiveDamage()
    {
        _isAttackGone = false;
        _animator.SetBool("isRangedFight", true);
        yield return new WaitForSeconds(1.5f);
        _isAttackGone = true;
        if (Random.Range(0, 6) != 1)
        {
            _selectedUnitInfo.SetHealth(_damage);
        }
        base.CheckUnitAlive();
    }

    protected override void AttackFortress()
    {
        base.AttackFortress();
        StartCoroutine(GiveDamageForFortress());
    }
    private IEnumerator GiveDamageForFortress()
    {
        GameObject rock;
        _isAttackGone = false;
        _animator.SetBool("isRangedFight", true);
        yield return new WaitForSeconds(1.5f);
        _isAttackGone = true;
        if (Random.Range(0, 6) != 1)
        {
            _fortInfo.SetFortressHealth(_damage);
            //rock = Instantiate(_rockPrefab, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity, transform);
            //_fortInfo.SetFortressHealth(_damage);
        }
        base.CheckFortressAlive();
    }
    private IEnumerator MoveRock(GameObject rock)
    {
        rock.transform.position = Vector3.MoveTowards(rock.transform.position, _selectedUnitInfo.transform.position, Time.fixedDeltaTime);
        yield return new WaitForSeconds(1f);
        Destroy(rock);
    }
}
