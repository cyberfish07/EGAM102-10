using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;

    public delegate void PlayerDeathEvent();
    public event PlayerDeathEvent OnPlayerDeath;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Invoke player death event
        OnPlayerDeath?.Invoke();
        // Keep to make UI enable
        gameObject.SetActive(false);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
