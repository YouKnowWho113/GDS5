using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject enemy1;

    public bool loading;
    public static bool GameIsPaused = false;
    bool isResuming = false;

    public KeyCode[] yesKeys;
    public KeyCode[] restartKeys;
    public KeyCode[] menuKeys;
    public RawImage[] yesImages;
    public RawImage[] restartImages;
    public RawImage[] menuImages;
    public int yesIndex = 0;
    public int restartIndex = 0;
    public int menuIndex = 0;

    public CanvasGroup blackScreen;

    public Text countdownText;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        enemy1.SetActive(false);
    }

    public void Update()
    {
        StartCoroutine(FadeIn());

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Alpha1) && !isResuming)
        {
            if (!GameIsPaused)
                PauseGame();

            else
            {
                if (Input.anyKeyDown)
                {
                    KeyCode yesKey = yesKeys[yesIndex];
                    KeyCode restartKey = restartKeys[restartIndex];
                    KeyCode menuKey = menuKeys[menuIndex];

                    if (Input.GetKeyDown(yesKey))
                    {
                        Debug.Log("Correct: " + yesKey);

                        OnYesInput();

                        Resume();
                    }

                    else if (Input.GetKeyDown(restartKey))
                    {
                        Debug.Log("Correct: " + restartKey);

                        OnRestartInput();

                        StartCoroutine(CheckRestart());
                    }

                    else if (Input.GetKeyDown(menuKey))
                    {
                        Debug.Log("Correct: " + menuKey);

                        OnMenuInput();

                        GoToMenu();
                    }
                    else
                    {
                        FailYes();
                        FailRestart();
                        FailMenu();
                        yesIndex = 0;
                        restartIndex = 0;
                        menuIndex = 0;
                    }
                }
            }
        }
    }

    IEnumerator FadeIn()
    {
        while (blackScreen.alpha > 0f)
        {
            loading = true;
            blackScreen.alpha -= 0.25f * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);
        loading = false;
        yield return new WaitForSeconds(1f);
        enemy1.SetActive(true);
    }

    void PauseGame()
    {
        if (!loading)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    }

    public void OnYesInput()
    {
        if (yesIndex < yesImages.Length)
        {
            yesImages[yesIndex].gameObject.SetActive(true);
            yesIndex++;
        }
    }

    public void Resume()
    {
        if (yesIndex >= yesImages.Length)
        {
            StartCoroutine(ResumeRoutine());
        }
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
        yesIndex = 0;
        yesImages[yesImages.Length].gameObject.SetActive(false);
        GameIsPaused = false;

        isResuming = false;
    }

    public void FailYes()
    {
        for (int i = 0; i < yesImages.Length; i++)
        {
            yesImages[i].gameObject.SetActive(false);
        }
    }

    public void OnRestartInput()
    {
        if (restartIndex < restartImages.Length)
        {
            restartImages[restartIndex].gameObject.SetActive(true);
            restartIndex++;
        }
    }

    IEnumerator CheckRestart()
    {
        if (restartIndex >= restartKeys.Length)
        {
            while (blackScreen.alpha < 1f)
            {
                blackScreen.alpha += 0.75f * Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.75f);
            Debug.Log("Restart The Game!");
            SceneManager.LoadSceneAsync(1);
        }
    }

    public void FailRestart()
    {
        for (int i = 0; i < restartImages.Length; i++)
        {
            restartImages[i].gameObject.SetActive(false);
        }
    }

    public void OnMenuInput()
    {
        if (menuIndex < menuImages.Length)
        {
            menuImages[menuIndex].gameObject.SetActive(true);
            menuIndex++;
        }
    }

    public void GoToMenu()
    {
        if (menuIndex >= menuKeys.Length)
        {
            Time.timeScale = 1f;
            GameIsPaused = false;

            SceneManager.LoadScene("Main Menu");
        }
    }

    public void FailMenu()
    {
        for (int i = 0; i < menuImages.Length; i++)
        {
            menuImages[i].gameObject.SetActive(false);
        }
    }
}
