using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedTick()
    {
        base.FixedTick();
    }

    public override void Tick()
    {
        base.Tick();
    }

    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Enemy thinking...");
        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Enemy performs action");

        //StateMachineBehaviour.ChangeState<PlayerTurnGameState>();
    }
}
