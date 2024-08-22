using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private string menuScene, gameScene;

    private bool lost;
    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        lost = false;
        Time.timeScale = 1;
        isPaused = false;
        pauseScreen.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && !lost)
        {

            PauseGame();
        }
    }

    public void LoseGame()
    {
        lost = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        isPaused = true;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        isPaused = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadWorldScene()
    {

        SceneManager.LoadScene("worldScene");
    }
}
