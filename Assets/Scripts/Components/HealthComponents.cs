using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponents : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 3;
    private int Health;

    void Awake()
    {
        Health = MaxHealth;
    }

    public void SetHealth(int newHealth)
    {
        Health = Mathf.Clamp(newHealth, 0, MaxHealth);
    }

    public int GetHealth()
    {
        return Health;
    }

    public void DecHealth()
    {
        Health = Mathf.Clamp(Health - 1, 0, MaxHealth);
    }

    public void IncHealth()
    {
        Health = Mathf.Clamp(Health + 1, 0, MaxHealth);
    }

    public bool IsDead()
    {
        return Health == 0;
    }
}