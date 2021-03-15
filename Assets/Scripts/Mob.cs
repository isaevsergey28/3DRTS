using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    protected bool _isTriggeredOnMob = false;
    protected bool _isAttackGone = true;
    protected bool _isGameOff = false;

    [SerializeField] protected float _attackRadius = 10f;
    [SerializeField] protected float _stopRadiusForMob;

    protected Animator _animator;
    protected int _damage = 1;

    protected void OffFightAnim()
    {
        _animator.SetBool("isMeleeFight", false);
        _animator.SetBool("isRangedFight", false);
    }
    protected void OnFightAnim()
    {
        _animator.SetBool("isMeleeFight", true);
        _animator.SetBool("isRangedFight", true);
    }

}
