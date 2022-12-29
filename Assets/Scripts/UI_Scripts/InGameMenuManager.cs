using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    public static bool isGamePaused = true;

    public void StartNewGame()
    {
       

        Stove.woodCount = 0;
        PlayerInventory.inventory = 0;

        Stove.stoveLoad = 0;

        isGamePaused = true;
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadScene(1, LoadSceneMode.Single); // 1 -> initial game scene
        Cursor.lockState = CursorLockMode.Locked;

      
    }

    public void ResumeGame()
    {
        isGamePaused = true;
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(2);
        Cursor.lockState = CursorLockMode.Locked;// 2 -> InGameUI menu scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void GamePause()
    {
        if (isGamePaused)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        }
       isGamePaused = false;
       Time.timeScale = 0;
      
    }


    private void OnEnable()
    {
        PlayerController.gamePaused += GamePause;
    }

    private void OnDisable()
    {
        PlayerController.gamePaused -= GamePause;
    }
}
