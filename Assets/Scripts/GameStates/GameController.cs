using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject _playerHud;
    [SerializeField] GameObject _enemyHud;
    [SerializeField] GameObject _winNLoseHud;
    [SerializeField] GameObject _swapHud;
    [SerializeField] GameObject _winTxt;
    [SerializeField] GameObject _loseTxt;
    [SerializeField] GameObject _permHUD;

    [SerializeField] AudioClip _winSound;
    [SerializeField] AudioClip _loseSound;
    [SerializeField] AudioClip _defendSound;

    [SerializeField] public GameObject _currentEnemy;
    [SerializeField] public GameObject[] _EnemyList;
    [SerializeField] public GameObject[] _EnemyHPList;
    [SerializeField] public Health _player;

    [SerializeField] public ParticleSystem _playerAttackParticles;

    private bool _check= true;
    public int _enemyNum = 0;
    public void PlayerTurnHUDOn()
    {
        _playerHud.SetActive(true);
    }
    public void PlayerTurnHUDOff()
    {
        _playerHud.SetActive(false);
    }
    public void EnemyTurnHUDOn()
    {
        _enemyHud.SetActive(true);
    }
    public void EnemyTurnHUDOff()
    {
        _enemyHud.SetActive(false);
    }
    public void WinNLoseHUDOn()
    {
        _winNLoseHud.SetActive(true);
    }
    public void WinNLoseHUDOff()
    {
        _winNLoseHud.SetActive(false);
    }
    public void SwapHUDOn()
    {
        _swapHud.SetActive(true);
    }
    public void SwapHUDOff()
    {
        _swapHud.SetActive(false);
    }

    public void WinTextOn()
    {
        _winTxt.SetActive(true);
        _permHUD.SetActive(false);
    }
    public void LoseTextOn()
    {
        _loseTxt.SetActive(true);
        _permHUD.SetActive(false);
    }

    public void AudioFeedbackWin()
    {
        //audio. TODO - consider Object Pooling for performance
        if (_winSound != null)
        {
            AudioHelper.PlayClip2D(_winSound, 1f);
        }
    }

    public void AudioFeedbackLose()
    {
        //audio. TODO - consider Object Pooling for performance
        if (_loseSound != null)
        {
            AudioHelper.PlayClip2D(_loseSound, 1f);
        }
    }

    public void AudioFeedbackDefend()
    {
        if(_defendSound != null)
        {
            AudioHelper.PlayClip2D(_defendSound, 1f);
        }
    }

    public void EnemySwap()
    {
        if (_check) 
        {
            _currentEnemy.SetActive(false);
        }
        else
        {
            _currentEnemy.SetActive(true);
            _enemyNum++;
        }
        _check = !_check;
    }
}
