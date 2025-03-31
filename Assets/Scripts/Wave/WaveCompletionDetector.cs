using UnityEngine;
using System.Collections;

public class WaveCompletionDetector : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private float checkInterval = 1f;
    [SerializeField] private float completionDelay = 3f;
    
    private bool completionCheckActive = false;
    private float noEnemyTime = 0f;

    private void Start()
    {
        if (waveManager == null)
            waveManager = FindObjectOfType<WaveManager>();
        
        // Start checking for enemies
        StartCoroutine(CheckEnemies());
    }

    private IEnumerator CheckEnemies()
    {
        while (true)
        {
            // Count enemies
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            
            // If no enemies
            if (enemies.Length == 0)
            {
                if (!completionCheckActive)
                {
                    completionCheckActive = true;
                    noEnemyTime = 0f;
                }
                else
                {
                    noEnemyTime += checkInterval;
                    
                    // If no enemies for completion delay, level complete
                    if (noEnemyTime >= completionDelay)
                    {
                        LevelComplete();
                        break;
                    }
                }
            }
            else
            {
                completionCheckActive = false;
            }
            
            yield return new WaitForSeconds(checkInterval);
        }
    }

    private void LevelComplete()
    {
        // Show victory UI
        UIManager.Instance?.ShowVictoryUI();
    }
}
