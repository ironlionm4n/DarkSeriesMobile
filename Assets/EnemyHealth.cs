using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = startingHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Bullet>(out var bullet))
        {
            _currentHealth -= bullet.Damage;
            Debug.Log(_currentHealth);
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
