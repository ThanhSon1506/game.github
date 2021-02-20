using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState
{
     private float attackTimer;
    private float attackCooldown=3;
    private bool  canAttack=true;
    private Enemy enemy;
    public void Execute()
    {
        Attack();
        if(enemy.InThrowRange&&!enemy.InMeleeRange)
        {
            enemy.ChangeSate(new RangedSates());
        }
        else if(enemy.Target==null)
        {
            enemy.ChangeSate(new IdleState());
        }
    }

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }
    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
             canAttack = true;
            attackTimer = 0;
        }
        if ( canAttack)
        {
             canAttack = false;
            enemy.myAnimator.SetTrigger("attack");
        }
    }

}
