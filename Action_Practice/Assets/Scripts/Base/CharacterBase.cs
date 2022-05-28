using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected Rigidbody2D myRigid;
    protected SpriteRenderer myRenderer;
    protected Animator myAni;

    protected virtual void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    protected enum FSM{
        idle, move, attack, die, hit
    }

    [SerializeField]
    protected FSM state;

    [SerializeField]
    protected float moveSpeed, maxHp, curHp, damage;
    
    [SerializeField]
    protected bool attack;

    protected virtual void Move()
    {

    }

    protected virtual void Attack()
    {

    }

    protected virtual void Die()
    {
        if (!myAni.GetBool("Die"))
        {
            myAni.SetBool("Die", true);
        }
        else if (myAni.GetCurrentAnimatorStateInfo(0).IsName("Death") && myAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            myAni.SetBool("Die", false);
            Destroy(gameObject, 0.3f);
        }
    }

    public virtual void Hit(float damage)
    {
        curHp -= damage;
    }

    protected virtual void Idle()
    {
        
    }
    
}
