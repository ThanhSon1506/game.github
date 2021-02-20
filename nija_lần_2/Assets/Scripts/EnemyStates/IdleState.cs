using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;
    private float idleTimer;
    private float idleDuration=10;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }


    public void Execute()
    {
        Debug.Log("I'm Idling");
        Idle();
        if(enemy.Target!=null)
        {
            enemy.ChangeSate(new PatrolState());
        }
        
    }

   
    public void Exit()
    {
 
    }

    public void OnTriggerEnter(Collider2D other)
    {
 
    }
    private void Idle()
    {
        enemy.myAnimator.SetFloat("speed", 0);
        idleTimer += Time.deltaTime;

        if(idleTimer>=idleDuration)
        {
            enemy.ChangeSate(new PatrolState());
        }
    }
}
