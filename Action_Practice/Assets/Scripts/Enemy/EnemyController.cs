using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterBase
{
    [SerializeField]
    GameObject player;

    float h, forward = 1;
    Vector3 moveDir = Vector3.zero, playerPos;


    RaycastHit2D attackHit;

    protected override void Start()
    {
        base.Start();

        maxHp = 1;
        curHp = maxHp;
        damage = 1;

        moveSpeed = 2f;
    }

    void Update()
    {
        if(player == null)
        {
            state = FSM.idle;
        }
        else
        {
            playerPos = player.transform.position;

            h = 0;
            if (playerPos.x - transform.position.x > 1)
            {
                h = 1;
            }
            if (playerPos.x - transform.position.x < -1)
            {
                h = -1;
            }

            if (state != FSM.attack)
                attack = (h == 0);

            forward = (h == 0) ? forward : h;
            myAni.SetBool("isRun", (state == FSM.move));
        }

        SetState();

        switch (state)
        {
            case FSM.idle:
                break;

            case FSM.move:
                Move();
                break;

            case FSM.attack:
                Attack();
                break;

            case FSM.hit:
                break;

            case FSM.die:
                Die();
                break;
        }
    }

    private void SetState()
    {
        if (state != FSM.hit && state != FSM.die)
        {
            if (state == FSM.attack && !attack)
                state = FSM.idle;

            if (attack)
                state = FSM.attack;
            else
                state = FSM.move;
        }

        if (curHp != maxHp)
        {
            maxHp = curHp;
            state = FSM.hit;
        }

        if (maxHp <= 0)
            state = FSM.die;
    }

    protected override void Move()
    {
        moveDir.x = h;

        myRenderer.flipX = (h > 0);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    protected override void Attack()
    {
        if (!myAni.GetBool("Attack"))
        {
            if(attack)
            {
                Vector3 AttackingPoint = transform.position;
                AttackingPoint.y -= 1;
                attackHit = Physics2D.Raycast(AttackingPoint, Vector2.right * forward, 5f, LayerMask.GetMask("Player"));
                Debug.DrawRay(AttackingPoint, Vector2.right * forward);

                if (attackHit.collider != null)
                {
                    attackHit.collider.GetComponent<PlayerController>().Hit(damage);
                    myAni.SetBool("Attack", true);
                }
            }
        }
        else if (myAni.GetCurrentAnimatorStateInfo(0).IsName("Attack") && myAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            myAni.SetBool("Attack", false);
            attack = false;
        }
    }
}