using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameController))]
public class GameFSM : StateMachineMB
{
    private GameController _controller;


    //state variables here
    public GameSetupState SetupState { get; private set; }
    public EnemyTurnState EnemyTurnState { get; private set; }
    public PlayerTurnState PlayerTurnState { get; private set; }
    public WinState WinState { get; private set; }
    public LoseState LoseState { get; private set; }
    public SwapEnemyState SwapEnemyState { get; private set; }
    private void Awake()
    {
        _controller = GetComponent<GameController>();
        //state instantiation here
        SetupState = new GameSetupState(this, _controller);
        EnemyTurnState = new EnemyTurnState(this, _controller);
        PlayerTurnState = new PlayerTurnState(this, _controller);
        WinState = new WinState(this, _controller);
        LoseState = new LoseState(this, _controller);
        SwapEnemyState = new SwapEnemyState(this, _controller);
    }

    private void Start()
    {
        ChangeState(SetupState);
    }

    public void EnemyState()
    {
        PlayerTurnState.Attack();
        ChangeState(EnemyTurnState);
    }

    public void WinTurnState()
    {
        if (_controller._player._currentHealth != _controller._player._maxHealth)
        {
            ChangeState(EnemyTurnState);
        }
        PlayerTurnState.Heal();
    }

    public void LoseTurnState()
    {
        PlayerTurnState.Defend();
        ChangeState(EnemyTurnState);
    }
}
