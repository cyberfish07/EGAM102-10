using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float launchForce = 15f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private int maxBalls = 5;

    private float cooldownTimer = 0f;
    private int activeBalls = 0;
    private Vector3 mouseWorldPosition;

    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateMousePosition();
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(1) && cooldownTimer <= 0 && activeBalls < maxBalls)
        {
            LaunchBall();
            cooldownTimer = cooldown;
        }
    }

    private void UpdateMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPosition.z = 0f;
    }

    private void LaunchBall()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (mouseWorldPosition - transform.position).normalized;
            rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
        }
        activeBalls++;

        DestructibleObject destructible = ball.GetComponent<DestructibleObject>();
        if (destructible != null)
        {
            destructible.OnDestroyed += OnBallDestroyed;
        }
    }

    private void OnBallDestroyed()
    {
        activeBalls--;
    }
}