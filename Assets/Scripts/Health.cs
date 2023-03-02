using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] public int _maxHealth = 100;
    [SerializeField] public int _currentHealth = 100;

    [SerializeField] GameObject _damagedPanel = null;
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log("Took: " + damage);
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
}
