using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : Pawn
{
    bool isAttack;
    LayerMask layerGround;

    bool flip, isJumping;

    float h;
    Vector3 posAfterJump;
    float jumpPower;

    void Start()
    {
        myAni = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        myRigid = GetComponent<Rigidbody2D>();

        jumpPower = 8f;
        layerGround = LayerMask.NameToLayer("Ground");

        moveSpeed = 5f;
        hp = 100;
        damage = 10;

        state = State.idle;
        isAttack = false;
        isJumping = false;

        dir = Vector3.zero;
    }

    void Update()
    {
        if(!isAttack)
        {
            Run();
            Jump();
        }

        Attack();
    }

    protected override void Attack()
    {
        if(isAttack)
        {
            if(myAni.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                isAttack = false;
                myAni.SetBool("Attack", false);
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Z))
        {
            myAni.SetBool("Attack", true);
            isAttack = true;
        }
    }

    protected override void Hit()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f, 1<<6);

        if(hit.collider != null)
        {
           hit.transform.GetComponent<EnemyPawn>().Hurt(this.damage);
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
        h = Input.GetAxisRaw("Horizontal");

        myAni.SetBool("isRun", (h != 0) && !isJumping);

        if(flip && h > 0 || !flip && h < 0)
        {
            flip = !flip;
            myRenderer.flipX = flip;
        }

        dir.x = h;
        transform.position += (dir * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if(!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            myRigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            myAni.SetBool("Jump", true);
        }
        
        if(isJumping && Physics2D.Raycast(transform.position, Vector2.down, 100f, layerGround))
        {
            Debug.Log("Land");
            isJumping = false;
        }

        if(myAni.GetBool("Jump") && transform.position.y < posAfterJump.y)
        {
            myAni.SetBool("Jump", false);
            myAni.SetTrigger("JumpFall");
        }
        posAfterJump = transform.position;
    }
}
