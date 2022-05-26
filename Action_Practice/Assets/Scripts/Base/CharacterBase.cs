using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected enum FSM{
        idle, move, attack, die, hit
    }

    [SerializeField]
    protected FSM state;

    [SerializeField]
    protected float moveSpeed, hp, damage;
    
    [SerializeField]
    protected bool isAttackSwing;

    protected virtual void Move()
    {
        
    }

    protected virtual void Attack()
    {
        
    }

    public virtual void Hit(float damage)
    {
        
    }

    protected virtual void Idle()
    {
        
    }
    
}
