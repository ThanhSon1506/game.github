using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy;
    private float patrolTimer;
    private float patrolDuration = 10;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("Patroling");
        Patrol();
        enemy.Move();

        if(enemy.Target!=null&&enemy.InThrowRange)
        {
            enemy.ChangeSate(new RangedSates());
        }
    }

   
    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
       if(other.tag=="Edge")
       {
           enemy.changeDirection();
       }
    }
    private void Patrol()
    {
      
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeSate(new PatrolState());
        }
    }
}
