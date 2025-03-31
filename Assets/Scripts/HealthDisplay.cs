using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image[] healthImages;
    [SerializeField] private Sprite fullHealthSprite;
    [SerializeField] private Sprite emptyHealthSprite;

    private void Start()
    {
        UpdateHealthDisplay();
    }

    private void Update()
    {
        UpdateHealthDisplay();
    }

    private void UpdateHealthDisplay()
    {
        if (playerHealth == null || healthImages.Length == 0) return;

        int currentHealth = playerHealth.GetCurrentHealth();

        // Update health images
        for (int i = 0; i < healthImages.Length; i++)
        {
            if (i < currentHealth)
            {
                healthImages[i].sprite = fullHealthSprite;
                healthImages[i].enabled = true;
            }
            else
            {
                if (emptyHealthSprite != null)
                {
                    healthImages[i].sprite = emptyHealthSprite;
                }
                else
                {
                    healthImages[i].enabled = false;
                }
            }
        }
    }
}
