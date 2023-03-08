using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    private int _turnsTaken = 0;

    public Health _enemy;


    public PlayerTurnState(GameFSM stateMachine, GameController controller)
    {
        //hold on to our parameters in our class variables for reuse
        _stateMachine = stateMachine;
        _controller = controller;
    }
    public override void Enter()
    {
        Debug.Log("Entering Player Turn");
        _controller.PlayerTurnHUDOn();
        if(_turnsTaken == 0)
        {
            _enemy = _controller._currentEnemy.GetComponent<Health>();
        }
        _turnsTaken++;
        Debug.Log("Turns Taken: " + _turnsTaken);
        base.Enter();
    }

    public override void Exit()
    {
        _controller.PlayerTurnHUDOff();
        if (_enemy._currentHealth <= 0)
        {
            _stateMachine.ChangeState(_stateMachine.SwapEnemyState);
            _turnsTaken = 0;
        }
        base.Exit();
    }

    public override void FixedTick()
    {
        base.FixedTick();
    }

    public override void Tick()
    {
        base.Tick();
        if (_enemy._currentHealth <=0) 
        { 
            _stateMachine.ChangeState(_stateMachine.SwapEnemyState);
            _turnsTaken = 0;
        }
    }

    public void Attack()
    {
        Debug.Log("Player Attacked Enemy");
        _enemy.TakeDamage(50);
        _stateMachine.ChangeState(_stateMachine.EnemyTurnState);
    }

    public void Defend()
    {
        Debug.Log("Player Defended");
        if (!_controller._player._playerIsDefend)
        {
            _controller._player._playerIsDefend = true;
            _stateMachine.ChangeState(_stateMachine.EnemyTurnState);
        }
    }

    public void Heal()
    {
        Debug.Log("Player Healed");
        _controller._player.Heal(15);
        _stateMachine.ChangeState(_stateMachine.EnemyTurnState);
    }
}
