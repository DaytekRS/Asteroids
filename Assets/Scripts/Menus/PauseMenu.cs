using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause;

    public GameObject pauseMenuUI;

    private void Awake()
    {
        GameIsPause = false;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameIsPause = !GameIsPause;
            pauseMenuUI.SetActive(GameIsPause);
            Time.timeScale = GameIsPause ? 0f : 1f;
        }
    }

    public void LoadMenu()
    {
        Awake();
        SceneManager.LoadScene(0);
    }
}