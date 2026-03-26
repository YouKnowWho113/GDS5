using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{

    public KeyCode[] startKeys;
    public KeyCode[] exitKeys;
    public RawImage[] startImages;
    public RawImage[] exitImages;
    public int startIndex = 0;
    public int exitIndex = 0;

    public CanvasGroup blackScreen;

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    
    public void Update()
    {
        if (Input.anyKeyDown)
        {
            KeyCode startKey = startKeys[startIndex];
            KeyCode exitKey = exitKeys[exitIndex];

            if (Input.GetKeyDown(startKey))
            {
                Debug.Log("Correct: " + startKey);

                OnStartInput();

                StartCoroutine(CheckStart());
            }

            else if (Input.GetKeyDown(exitKey))
            {
                Debug.Log("Correct: " + exitKey);

                OnExitInput();

                QuitGame();
            }
            else
            {
                FailStart();
                FailExit();
                startIndex = 0;
                exitIndex = 0;
            }
        }
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
