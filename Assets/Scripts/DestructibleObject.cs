using System;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] private float lifetime = 10f;
    [SerializeField] private int maxCollisions = 5;
    [SerializeField] private float damageAmount = 5f;
    
    private int collisionCount = 0;
    
    public event Action OnDestroyed;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionCount++;
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damageAmount);
        }

        if (collisionCount >= maxCollisions)
        {
            DestroyObject();
        }
    }

    private void OnDestroy()
    {
        // Invoke destroyed event
        OnDestroyed?.Invoke();
    }

    private void DestroyObject()
    {
        // Invoke destroyed event and destroy
        OnDestroyed?.Invoke();
        Destroy(gameObject);
    }
}
