using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public bool loading;

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

    public void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            BGMusic.instance.GetComponent<AudioSource>().Pause();
        }

        StartCoroutine(FadeIn());

        if (Input.anyKeyDown)
        {

            KeyCode yesKey = yesKeys[yesIndex];
            KeyCode restartKey = restartKeys[restartIndex];
            KeyCode menuKey = menuKeys[menuIndex];

            if (Input.GetKeyDown(yesKey) && yesKeys.Length > 1)
            {
                Debug.Log("Correct: " + yesKey);

                OnYesInput();

                StartCoroutine(Resume());
            }

            else if (Input.GetKeyDown(restartKey) && restartKeys.Length > 1)
            {
                Debug.Log("Correct: " + restartKey);

                OnRestartInput();

                StartCoroutine(CheckRestart());
            }

            else if (Input.GetKeyDown(menuKey) && menuKeys.Length > 1)
            {
                Debug.Log("Correct: " + menuKey);

                OnMenuInput();

                StartCoroutine(GoToMenu());
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


    IEnumerator FadeIn()
    {
        if (yesIndex < yesKeys.Length && restartIndex < restartKeys.Length && menuIndex < menuKeys.Length)
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

    public void OnYesInput()
    {
        if (yesIndex < yesImages.Length)
        {
            yesImages[yesIndex].gameObject.SetActive(true);
            yesIndex++;
        }
    }
    
    IEnumerator Resume()
    {
        if (yesIndex >= yesKeys.Length)
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
    

    public void FailYes()
    {
        if (yesIndex != yesImages.Length)
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

    IEnumerator GoToMenu()
    {
        if (menuIndex >= menuKeys.Length)
        {
            while (blackScreen.alpha < 1f)
            {
                blackScreen.alpha += 0.75f * Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.75f); 
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
