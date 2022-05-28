using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{
    float h, forward = 1;
    Vector3 moveDir = Vector3.zero;

    [SerializeField]
    bool isCanJump = false;

    float jumpPower;
    RaycastHit2D jumpHit, attackHit;

    protected override void Start()
    {
        base.Start();

        maxHp = 2;
        curHp = maxHp;
        damage = 1;

        myRenderer = GetComponent<SpriteRenderer>();
        myAni = GetComponent<Animator>();

        moveSpeed = 10f;
        jumpPower = 5f;
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        if(state != FSM.attack)
            attack = Input.GetKeyDown(KeyCode.Z);
        forward = (h == 0) ? forward : h;

        myAni.SetBool("isRun", (state == FSM.move));

        SetState();

        Jump();

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
        if(state != FSM.hit && state != FSM.die)
        {
            if (state == FSM.attack && !attack)
                state = FSM.idle;

            if (attack)
                state = FSM.attack;

            if (state != FSM.attack)
            {
                if(h == 0)
                {
                    state = FSM.idle;
                }
                else
                {
                    state = FSM.move;
                }
            }
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

        myRenderer.flipX = (h < 0);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void Jump()
    {
        if (isCanJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myRigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isCanJump = false;
            }
        }
        else
        {
            jumpHit = Physics2D.Raycast(transform.position, Vector2.down, 0.9f, LayerMask.GetMask("Ground"));


            if (jumpHit.collider != null)
            {
                isCanJump = true;
            }
        }
    }

    protected override void Attack()
    {
        if (!myAni.GetBool("Attack"))
        {
            attackHit = Physics2D.Raycast(transform.position, Vector2.right * forward, 1f, LayerMask.GetMask("Enemy"));
            if (attackHit.collider != null)
            {
                attackHit.collider.GetComponent<EnemyController>().Hit(damage);
            }
            myAni.SetBool("Attack", true);
        }
        else if (myAni.GetCurrentAnimatorStateInfo(0).IsName("Attack") && myAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            myAni.SetBool("Attack", false);
            attack = false;
        }
    }
}
