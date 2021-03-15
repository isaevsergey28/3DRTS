using System;
using UnityEngine;
using Zenject;

public class EnemyBehaviour : Mob
{
    private GameObject _fortress;
    protected FortressInfo _fortInfo;

    private ActiveUnits _activeUnits;
    private int _activeUnitsCount;

    private GameObject _unitToAttack;
    protected UnitInfo _selectedUnitInfo;

    [SerializeField] protected float _stopRadiusForFortress = 3;

    private void Start()
    {
        
        _animator = GetComponent<Animator>();
        _fortress = GameObject.Find("Fortress");
        _fortInfo = _fortress.GetComponent<FortressInfo>();
    }
    [Inject]
    private void Construct(ActiveUnits activeUnits)
    {
        _activeUnits = activeUnits;
    }

    private void Update()
    {
        if(!_isGameOff)
        {
            if (!_isTriggeredOnMob)
            {
                Vector3 directionFortress = _fortress.transform.position - transform.position;
                if (directionFortress.magnitude > _stopRadiusForFortress)
                {
                    MoveAndLookAtOnFortress();
                }
                else
                {
                    if (_isAttackGone)
                    {
                        _animator.SetBool("isWalking", false);
                        AttackFortress();
                    }
                }
                CheckAllUnits();
            }
            else if (_unitToAttack)
            {
                if (_isAttackGone)
                {
                    OffFightAnim();
                    CheckUnitDistance(_unitToAttack);
                    AttackUnit(_unitToAttack);
                }
            }
            else
            {
                _isTriggeredOnMob = false;
                OffFightAnim();
            }
        }
    }

    protected virtual void AttackFortress()
    {
        OnFightAnim();
        transform.LookAt(_fortress.transform.position);
    }

    private void CheckAllUnits()
    {
        _activeUnitsCount = _activeUnits.GetUnits().Count;
        foreach (GameObject unit in _activeUnits.GetUnits())
        {
            Vector3 directionUnit = unit.transform.position - transform.position;
            if (directionUnit.magnitude < _attackRadius)
            {
                _isTriggeredOnMob = true;
                _unitToAttack = unit;
                _selectedUnitInfo = _unitToAttack.GetComponent<UnitInfo>();
            }
        }
    }

    private void MoveAndLookAtOnFortress()
    {
        _animator.SetBool("isWalking", true);
        transform.position = Vector3.MoveTowards(transform.position, _fortress.transform.position, Time.fixedDeltaTime);
        transform.LookAt(_fortress.transform.position);
    }

    private void CheckUnitDistance(GameObject unitToAttack)
    {
        Vector3 direction = unitToAttack.transform.position - transform.position;
        if (direction.magnitude > _attackRadius)
        {
            _isTriggeredOnMob = false;
            _unitToAttack = null;
            OffFightAnim();
        }

    }

   
    protected virtual void AttackUnit(GameObject unitToAttack)  { }

    protected void CheckUnitAlive()
    {
        if(_selectedUnitInfo.GetHealth() <= 0)
        {
            _activeUnits.DeleteUnit(_unitToAttack);
            Destroy(_unitToAttack);
            OffFightAnim();
        }
    }
    protected void CheckFortressAlive()
    {
        if (_fortInfo.GetHealth() <= 0)
        {
            _isGameOff = true;
            OffFightAnim();
            _animator.SetBool("isWalking", false);
            _fortInfo.CreateExplosion();
            
        }
    }
}
