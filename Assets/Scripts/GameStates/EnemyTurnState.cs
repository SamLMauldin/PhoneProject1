using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class EnemyTurnState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;
    private float _pauseDuration = 5;
    public int _enemyDamage = 10;

    public EnemyTurnState(GameFSM stateMachine, GameController controller)
    {
        //hold on to our parameters in our class variables for reuse
        _stateMachine = stateMachine;
        _controller = controller;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("EnemyTurnStarts");
        Debug.Log("Enemy thinking...");
        _controller.EnemyTurnHUDOn();
    }

    public override void Exit()
    {
        Debug.Log("Leaving Enemy Turn");
        _controller.EnemyTurnHUDOff();
        base.Exit();
    }

    public override void FixedTick()
    {
        base.FixedTick();
        
    }

    public override void Tick()
    {
        base.Tick();
        if(StateDuration >= _pauseDuration)
        {
            Debug.Log("Enemy performs action");
            EnemyBasicAttack();
            _stateMachine.ChangeState(_stateMachine.PlayerTurnState);
        }
    }

    public void EnemyBasicAttack()
    {
        _controller._player.TakeDamage(_enemyDamage);
    }
}
