using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapEnemyState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;
    private float _pauseDuration = 1.5f;
    private int _nextEnemy = 0;
    public SwapEnemyState(GameFSM stateMachine, GameController controller)
    {
        //hold on to our parameters in our class variables for reuse
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        Debug.Log("Swapping Enemy");
        _controller.SwapHUDOn();
        _controller.EnemySwap();
        base.Enter();
    }

    public override void Exit()
    {
        Debug.Log("Returning to Normal Gameplay");
        _nextEnemy++;
        Debug.Log(_nextEnemy);
        Debug.Log(_controller._EnemyList.Length);
        _controller.SwapHUDOff();
        _controller.EnemySwap();
        base.Exit();
    }

    public override void FixedTick()
    {
        base.FixedTick();
    }

    public override void Tick()
    {
        if(_nextEnemy == _controller._EnemyList.Length)
            {
            _stateMachine.ChangeState(_stateMachine.WinState);
        }
        base.Tick();
        if (StateDuration >= _pauseDuration)
        {
            Debug.Log("Enemy got swapped to another");
            _controller._currentEnemy = _controller._EnemyList[_nextEnemy];
            _controller._EnemyHPList[_nextEnemy].SetActive(true);
            _stateMachine.ChangeState(_stateMachine.PlayerTurnState);
        }
    }

    public void EnemyAttack()
    {
        Debug.Log("Enemy Attacked");
    }
}
