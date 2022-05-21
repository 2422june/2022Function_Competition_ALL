using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPawn : Pawn
{
    bool isAttack;

    void Start()
    {
        hp = 100;
        damage = 10;
        state = State.idle;
        isAttack = false;
        myAni = GetComponent<Animator>();
    }

    void Update()
    {
        if(!isAttack)
        {

        }
    }

    protected override void Attack()
    {
        
    }

    protected override void Hit()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f, 1<<7);

        if(hit.collider != null)
        {
           hit.transform.GetComponent<PlayerPawn>().Hurt(this.damage);
        }
    }

    public override void Hurt(float damage)
    {
        hp-= damage;
        if(hp < 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        
    }

    protected override void Run()
    {
        
    }

}
