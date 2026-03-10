using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject lastSelectedElement;

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    
    public void Settings()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetElement()
    {
        eventSystem = FindObjectOfType<EventSystem>();

        if (!eventSystem)
        {
            return;
        }

        lastSelectedElement = eventSystem.firstSelectedGameObject;
    }

    public void Update()
    {
        if (!eventSystem)
        {
            return;
        }

        if (eventSystem.currentSelectedGameObject && lastSelectedElement != eventSystem.currentSelectedGameObject)
        {
            lastSelectedElement = eventSystem.currentSelectedGameObject;
        }

        if (!eventSystem.currentSelectedGameObject && lastSelectedElement)
        {
            eventSystem.SetSelectedGameObject(lastSelectedElement);
        }
    }
}
