using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private string menuSceneName = "MainMenu";
    private Button menuButton;

    private void Start()
    {
        menuButton = GetComponent<Button>();
        if (menuButton != null)
        {
            menuButton.onClick.AddListener(ReturnToMenu);
        }
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    private void OnDestroy()
    {
        if (menuButton != null)
        {
            menuButton.onClick.RemoveListener(ReturnToMenu);
        }
    }
}