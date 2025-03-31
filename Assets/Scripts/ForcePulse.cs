using UnityEngine;

public class ForcePulse : MonoBehaviour
{
    [SerializeField] private float forceStrength = 20f;
    [SerializeField] private float pulseRadius = 5f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private LayerMask affectedLayers = ~0;
    [SerializeField] private GameObject pulseEffectPrefab;

    private float cooldownTimer = 0f;

    private void Update()
    {
        // Cooldown timer
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && cooldownTimer <= 0)
        {
            EmitForcePulse();
            cooldownTimer = cooldown;
        }
    }

    private void EmitForcePulse()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pulseRadius);
        Debug.Log("Found " + colliders.Length + " objects in pulse radius");

        foreach (Collider2D hit in colliders)
        {
            if (hit.transform == transform)
                continue;

            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (hit.transform.position - transform.position).normalized;
                float distance = Vector2.Distance(transform.position, hit.transform.position);

                float forceFactor = 1.5f - (distance / pulseRadius);
                Vector2 force = direction * forceStrength * forceFactor * 2f;

                rb.AddForce(force, ForceMode2D.Impulse);

                if (hit.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(3f);
                    }
                }
            }
        }

        if (pulseEffectPrefab != null)
        {
            GameObject effect = Instantiate(pulseEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }
}