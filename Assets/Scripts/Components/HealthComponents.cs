using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponents : MonoBehaviour
{
    [SerializeField] private  float MaxHealth = 3f;
    private float Health;
        
    void Awake()
    {
        Health = MaxHealth;
    }

    public void SetHealth(float newHealth)
    {
        Health = Mathf.Clamp(newHealth, 0, MaxHealth);
    }

    public float GetHealth()
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
        return Health.Equals(0f);
    }

}
