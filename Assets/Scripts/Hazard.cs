using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Damage player
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }

        if (collision.CompareTag("PhysicsObject"))
        {
            Destroy(collision.gameObject);
        }
    }
}
