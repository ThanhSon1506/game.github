﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSates : IEnemyState
{
    private Enemy enemy;
    private float throwTimer;
    private float throwCoolDown=3;
    private bool canThrow=true;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }


    public void Execute()
    {
        ThrowKnife();
        if(enemy.InMeleeRange)
        {
            enemy.ChangeSate(new MeleeState());
        }
        else if(enemy.Target!=null)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeSate(new IdleState());
        }
    }

   
    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }
    private void ThrowKnife()
    {
        throwTimer += Time.deltaTime;
        if(throwTimer>=throwCoolDown)
        {
            canThrow = true;
            throwTimer = 0;
        }
        if(canThrow)
        {
            canThrow = false;
            enemy.myAnimator.SetTrigger("throw");
        }
    }
}
