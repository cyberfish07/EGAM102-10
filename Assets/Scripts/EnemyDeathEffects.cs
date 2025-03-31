using UnityEngine;

public class EnemyDeathEffects : MonoBehaviour
{
    [SerializeField] private GameObject Particle;
    [SerializeField] private AudioClip Sound;
    [SerializeField] private float volume = 1.0f;

    private Enemy enemy;
    private AudioSource audioSource;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.OnDeath += PlayDeathEffects;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    private void PlayDeathEffects()
    {
        // Play particle effect
        if (Particle != null)
        {
            GameObject particles = Instantiate(Particle, transform.position, Quaternion.identity);
            Destroy(particles, 2f);
        }

        // Play sound effect
        if (Sound != null && audioSource != null)
        {
            AudioSource.PlayClipAtPoint(Sound, transform.position, volume);
        }
    }

    private void OnDestroy()
    {
        if (enemy != null)
        {
            enemy.OnDeath -= PlayDeathEffects;
        }
    }
}