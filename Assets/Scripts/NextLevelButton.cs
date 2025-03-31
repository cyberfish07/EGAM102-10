using UnityEngine;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    private Button nextLevelButton;

    private void Start()
    {
        nextLevelButton = GetComponent<Button>();
        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.AddListener(LoadNextLevel);
        }
    }

    private void LoadNextLevel()
    {
        GameManager.Instance?.LoadNextLevel();
    }

    private void OnDestroy()
    {
        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.RemoveListener(LoadNextLevel);
        }
    }
}