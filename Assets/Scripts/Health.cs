using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] public int _maxHealth = 100;
    [SerializeField] public int _currentHealth = 100;

    [SerializeField] GameObject _damagedPanel = null;
    [SerializeField] GameObject _healPanel = null;
    [SerializeField] HealthBar _healthBar;

    [SerializeField] AudioClip _damagedSFX;

    [SerializeField] Camera _gameCamera = null;
    [SerializeField] Vector3 _originalPosOfCam;
    [SerializeField] float _shakeFrequency = 5;

    [SerializeField] ParticleSystem _damageParticles;

    private bool _damageTaken = false;
    public bool _playerIsDefend = false;

    void Awake()
    {
        _currentHealth = _maxHealth;
        if(_healthBar != null)
        {
            _healthBar.SetMaxHealth(_maxHealth);
        }
        if(_damagedPanel != null)
        {
            _damagedPanel.SetActive(false);
        }
        if(_gameCamera != null)
        {
            _originalPosOfCam = _gameCamera.transform.position;
        }
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if(_damagedPanel != null)
        {
            StartCoroutine(DamagePanel());
        }
        if(_gameCamera != null)
        {
            _damageTaken = true;
        }
        FeedBackDamaged();
        Debug.Log("Took: " + damage);
    }

    public void Heal(int damage)
    {
        _currentHealth += damage;
        if(_healPanel != null)
        {
            StartCoroutine(HealPanel());
        }
        Debug.Log("Healed: " + damage);

    }

    void Update()
    {
        if(_gameCamera != null && _damageTaken)
        {
            _gameCamera.transform.position = _originalPosOfCam + Random.insideUnitSphere * _shakeFrequency;
            if(_gameCamera != null)
            {
                StartCoroutine(CameraShake());
            }
        }
    }

    public void Kill()
    {
        Debug.Log("Dead");
    }

    IEnumerator DamagePanel()
    {
        _damagedPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _damagedPanel.SetActive(false);
    }

    IEnumerator HealPanel()
    {
        _healPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _healPanel.SetActive(false);
    }

    public void AudioFeedbackDamaged()
    {
        //audio. TODO - consider Object Pooling for performance
        if (_damagedSFX != null)
        {
            AudioHelper.PlayClip2D(_damagedSFX, 1f);
        }
    }

    IEnumerator CameraShake()
    {
        yield return new WaitForSeconds(0.5f);
        _gameCamera.transform.position = _originalPosOfCam;
        _damageTaken = false;
    }

    private void FeedBackDamaged()
    {
        if(_damageParticles != null)
        {
            _damageParticles = Instantiate(_damageParticles, transform.position, Quaternion.identity);
        }
    }
}
