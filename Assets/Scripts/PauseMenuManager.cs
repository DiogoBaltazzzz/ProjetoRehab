using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject exercisePanel;

    private bool isPaused = false;

    private void Start()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);

        if (exercisePanel != null)
            exercisePanel.SetActive(true);

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                ResumeGame();
            else
                ShowPauseMenu();
        }
    }

    public void ShowPauseMenu()
    {
        isPaused = true;

        if (pauseMenu != null)
            pauseMenu.SetActive(true);

        if (exercisePanel != null)
            exercisePanel.SetActive(false);

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;

        if (pauseMenu != null)
            pauseMenu.SetActive(false);

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
        yield return new WaitForSeconds(2f);
        ShowPauseMenu();
    }
}