using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{
    float h, v;
    Vector3 moveDir = Vector3.zero;
    bool isCanJump = false;
    float jumpPower;
    RaycastHit2D hit;

    protected override void Start()
    {
        base.Start();
        moveSpeed = 10f;
        jumpPower = 5f;
    }

    void Update()
    {
        SetState();
        
        switch(state)
        {
            case FSM.idle:
                break;

            case FSM.move:
                Jump();
                Move();
                break;

            case FSM.attack:
                break;

            case FSM.hit:
                break;

            case FSM.die:
                break;
        }
    }

    private void SetState()
    {
        h = Input.GetAxis("Horizontal");
        //v = Input.GetAxis("Vertical");

        if (h != 0)
        {
            state = FSM.move;
        }
    }

    protected override void Move()
    {

        moveDir.x = h;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void Jump()
    {
        if(isCanJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myRigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isCanJump = false;
            }
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
            if(hit.collider != null && hit.collider.CompareTag("Ground"))
            {
                isCanJump = true;
            }
        }
    }
}
