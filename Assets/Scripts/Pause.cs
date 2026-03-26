using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.EventSystems;


public class Pause : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject firstButton;
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
        /*
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            BGMusic.instance.GetComponent<AudioSource>().Pause();
        }
        */
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(FadeIn());

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Alpha1) && !isResuming)
        {
            if (!GameIsPaused)
                PauseGame();
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
        if (enemy1 != null)
        {
            enemy1.SetActive(true);
        }
    }

    void PauseGame()
    {
        if (!loading)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;

            EventSystem.current.SetSelectedGameObject(firstButton);
        }
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