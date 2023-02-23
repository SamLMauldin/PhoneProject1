using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    private int _turnsTillSwapTEMP = 3;
    private int _turnsTaken = 0;

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
        _turnsTaken++;
        Debug.Log("Turns Taken: " + _turnsTaken);
        base.Enter();
    }

    public override void Exit()
    {
        _controller.PlayerTurnHUDOff();
        base.Exit();
    }

    public override void FixedTick()
    {
        base.FixedTick();
    }

    public override void Tick()
    {
        base.Tick();
        if (_turnsTaken >= _turnsTillSwapTEMP)
        {
            _turnsTaken = 0;
            _stateMachine.ChangeState(_stateMachine.SwapEnemyState);
        }
    }

    public void Attack()
    {
        Debug.Log("Player Attacked Enemy");
        _stateMachine.ChangeState(_stateMachine.EnemyTurnState);
    }
}
