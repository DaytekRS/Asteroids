using UnityEngine;

public class HealthComponents : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    private int _health;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void SetHealth(int newHealth)
    {
        _health = Mathf.Clamp(newHealth, 0, _maxHealth);
    }

    public int GetHealth()
    {
        return _health;
    }

    public void DecHealth()
    {
        SetHealth(_health - 1);
    }

    public void IncHealth()
    {
        SetHealth(_health + 1);
    }

    public bool IsDead()
    {
        return _health == 0;
    }
}