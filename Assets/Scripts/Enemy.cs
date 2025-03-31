using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 10f;
    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D rb;

    public event Action OnDeath;

    private Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            float currentVelocity = rb.linearVelocity.magnitude;

            if (currentVelocity < moveSpeed * 0.8f)
            {
                rb.AddForce(direction * moveSpeed * 2f, ForceMode2D.Force);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(1);
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}