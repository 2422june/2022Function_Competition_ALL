using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    protected float hp;
    protected float damage, moveSpeed;

    protected Animator myAni;
    protected SpriteRenderer myRenderer;
    protected Rigidbody2D myRigid;

    protected Vector3 dir;

    protected enum State{
        idle, attack, run, hit, hurt, die
    }
    protected State state;

    protected abstract void Attack();
    protected abstract void Run();
    protected abstract void Hit();
    public abstract void Hurt(float damage);
    protected abstract void Die();
}
