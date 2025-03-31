using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string[] levelScenes;
    [SerializeField] private int currentLevelIndex = 0;

    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayerManagement();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerManagement();
    }

    private void PlayerManagement()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.OnPlayerDeath += OnPlayerDeath;
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex < levelScenes.Length)
        {
            SceneManager.LoadScene(levelScenes[currentLevelIndex]);
        }
        else
        {
            UIManager.Instance?.ShowVictoryUI(true);
        }
    }

    public void TriggerVictory()
    {
        bool isLastLevel = (currentLevelIndex == levelScenes.Length - 1);
        UIManager.Instance?.ShowVictoryUI(isLastLevel);
    }

    private void OnPlayerDeath()
    {
        UIManager.Instance?.ShowGameOverUI();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}