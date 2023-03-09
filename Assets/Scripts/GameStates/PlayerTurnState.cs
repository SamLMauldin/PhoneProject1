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
        _enemy = _controller._currentEnemy.GetComponent<Health>();
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
        if(_controller._player._currentHealth <= 0)
        {
            _stateMachine.ChangeState(_stateMachine.LoseState);
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
            _controller.AudioFeedbackDefend();
            _stateMachine.ChangeState(_stateMachine.EnemyTurnState);
        }
    }

    public void Heal()
    {
        _controller._player.Heal(60);
        if ((_controller._player._currentHealth + 45) !<= _controller._player._maxHealth)
        {
            Debug.Log("Player Healed");
        }
    }
}
