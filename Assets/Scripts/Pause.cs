using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject firstButton;

    public static bool GameIsPaused = false;

    bool isResuming = false;

    public Text countdownText;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && !isResuming)
        {
            if (!GameIsPaused)
                PauseGame();
        }
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        
        EventSystem.current.SetSelectedGameObject(firstButton);

        
    }

    public void Resume()
    {
        StartCoroutine(ResumeRoutine());
    }

    IEnumerator ResumeRoutine()
    {
        isResuming = true;

        pauseMenuUI.SetActive(false);

        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        countdownText.gameObject.SetActive(false);

        Time.timeScale = 1f;
        GameIsPaused = false;

        isResuming = false;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;

        SceneManager.LoadScene("Main Menu");
    }
}