using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject exercisePanel;

    private void Start()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(true);

        if (exercisePanel != null)
            exercisePanel.SetActive(true);

        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(true);

        if (exercisePanel != null)
            exercisePanel.SetActive(true);

        Time.timeScale = 1f;
    }

    public void RestartExercise()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ShowPauseMenuAfterFinish()
    {
        StartCoroutine(OpenPauseMenuAfterDelay());
    }

    private IEnumerator OpenPauseMenuAfterDelay()
    {
        yield return new WaitForSecondsRealtime(2f);

        if (pauseMenu != null)
            pauseMenu.SetActive(true);
    }
}