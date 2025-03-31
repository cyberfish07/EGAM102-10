using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private Button restart;
    [SerializeField] private Button nextLevel;
    [SerializeField] private Button menu;

    private static UIManager instance;

    public static UIManager Instance
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
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);
        restart.onClick.AddListener(GameManager.Instance.RestartLevel);
        nextLevel.onClick.AddListener(GameManager.Instance.LoadNextLevel);
    }

    public void ShowGameOverUI()
    {
        gameOverPanel.SetActive(true);
    }

    public void ShowVictoryUI(bool isLastLevel = false)
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);

            NextLevelButton nextLevelBtn = victoryPanel.GetComponentInChildren<NextLevelButton>(true);
            if (nextLevelBtn != null)
                nextLevelBtn.gameObject.SetActive(!isLastLevel);

            MenuButton menuBtn = victoryPanel.GetComponentInChildren<MenuButton>(true);
            if (menuBtn != null)
                menuBtn.gameObject.SetActive(isLastLevel);
        }
    }
}