using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public bool loading;
    public KeyCode[] startKeys;
    public KeyCode[] testKeys;
    public KeyCode[] exitKeys;
    public RawImage[] startImages;
    public RawImage[] testImages;
    public RawImage[] exitImages;
    public int startIndex = 0;
    public int testIndex = 0;
    public int exitIndex = 0;

    public CanvasGroup blackScreen;

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    
    public void Update()
    {
        StartCoroutine(FadeIn());

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            BGMusic.instance.GetComponent<AudioSource>().Pause();
        }

        if (Input.anyKeyDown)
        {
            KeyCode startKey = startKeys[startIndex];
            KeyCode testKey = testKeys[testIndex];
            KeyCode exitKey = exitKeys[exitIndex];

            if (Input.GetKeyDown(startKey))
            {
                Debug.Log("Correct: " + startKey);

                OnStartInput();

                if (testIndex != 0 || exitIndex != 0)
                {
                    FailTest();
                    FailExit();
                    testIndex = 0;
                    exitIndex = 0;
                }

                StartCoroutine(CheckStart());
            }

            else if (Input.GetKeyDown(testKey) && exitIndex != 3)
            {
                Debug.Log("Correct: " + testKey);

                OnTestInput();

                if (startIndex != 0 || exitIndex != 0)
                {
                    FailStart();
                    FailExit();
                    startIndex = 0;
                    exitIndex = 0;
                }

                StartCoroutine(CheckTest());
            }

            else if (Input.GetKeyDown(exitKey))
            {
                Debug.Log("Correct: " + exitKey);

                OnExitInput();

                if (startIndex != 0 || testIndex != 0)
                {
                    FailStart();
                    FailTest();
                    startIndex = 0;
                    testIndex = 0;
                }

                QuitGame();
            }
            else
            {
                FailStart();
                FailTest();
                FailExit();
                startIndex = 0;
                testIndex = 0;
                exitIndex = 0;
            }
        }
    }

    IEnumerator FadeIn()
    {
        if (startIndex < startKeys.Length && testIndex < testKeys.Length && exitIndex < exitKeys.Length)
        {
            while (blackScreen.alpha > 0f)
            {
                loading = true;
                blackScreen.alpha -= 0.25f * Time.deltaTime;
                yield return null;
            }
        }
        yield return new WaitForSeconds(1.5f);
        loading = false;
        yield return new WaitForSeconds(1f);
    }

    public void OnStartInput()
    {
        if (startIndex < startImages.Length)
        {
            startImages[startIndex].gameObject.SetActive(true);
            startIndex++;
        }
    }

    IEnumerator CheckStart()
    {
        if (startIndex >= startKeys.Length)
        {
            while (blackScreen.alpha < 1f)
            {
                blackScreen.alpha += 0.75f * Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.75f);
            Debug.Log("Start The Game!");
            SceneManager.LoadSceneAsync(1);
        }
    }

    public void FailStart()
    {
        for (int i = 0; i < startImages.Length; i++)
        {
            startImages[i].gameObject.SetActive(false);
        }
    }

    public void OnTestInput()
    {
        if (testIndex < testImages.Length)
        {
            testImages[testIndex].gameObject.SetActive(true);
            testIndex++;
        }
    }

    IEnumerator CheckTest()
    {
        if (testIndex >= testKeys.Length)
        {
            while (blackScreen.alpha < 1f)
            {
                blackScreen.alpha += 0.75f * Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.75f);
            Debug.Log("Testing");
            SceneManager.LoadScene("Test");
        }
    }
    
    public void FailTest()
    {
        for (int i = 0; i < testImages.Length; i++)
        {
            testImages[i].gameObject.SetActive(false);
        }
    }
    
    public void OnExitInput()
    {
        if (exitIndex < exitImages.Length)
        {
            exitImages[exitIndex].gameObject.SetActive(true);
            exitIndex++;
        }
    }

    public void QuitGame()
    {
        if (exitIndex >= exitKeys.Length)
        {
            Debug.Log("Quited!");

            Application.Quit();

        }
    }
    
    public void FailExit()
    {
        for (int i = 0; i < exitImages.Length; i++)
        {
            exitImages[i].gameObject.SetActive(false);
        }
    }
    
}
