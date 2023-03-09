using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class EnemyTurnState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;
    private float _pauseDuration = 2.5f;
    public int _enemyDamage = 10;
    public Health _enemy;
    private int _turnNum = 0;
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
        _enemy = _controller._currentEnemy.GetComponent<Health>();
    }

    public override void Exit()
    {
        Debug.Log("Leaving Enemy Turn");
        _controller.EnemyTurnHUDOff();
        _turnNum++;
        base.Exit();
    }

    public override void FixedTick()
    {
        base.FixedTick();
        
    }

    public override void Tick()
    {
        base.Tick();
        if(_enemy._currentHealth <= 0)
        {
            _stateMachine.ChangeState(_stateMachine.SwapEnemyState);
        }
        if(StateDuration >= _pauseDuration)
        {
            Debug.Log("Enemy performs action");
            EnemyBasicAttack();
            _stateMachine.ChangeState(_stateMachine.PlayerTurnState);
        }
    }

    public void EnemyBasicAttack()
    {
        if (_turnNum == 0)
        {
            if (_controller._player._playerIsDefend == true)
            {
                Debug.Log("Played Didn't take a lot of damage");
                _controller._player.TakeDamage((_enemyDamage * _controller._enemyNum) / 3);
                _controller._player._playerIsDefend = false;
            }
            else
            {
                _controller._player.TakeDamage((_enemyDamage *_controller._enemyNum));
            }
        }
        else
        {
            if (_controller._player._playerIsDefend == true)
            {
                Debug.Log("Played Didn't take a lot of damage");
                _controller._player.TakeDamage(_enemyDamage * _controller._enemyNum / (3/2));
                _controller._player._playerIsDefend = false;
            }
            else
            {
                _controller._player.TakeDamage((_enemyDamage * _controller._enemyNum) * 2);
            }
            _turnNum = -1;
        }
    }
}
