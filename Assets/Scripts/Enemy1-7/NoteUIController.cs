using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteUIController : MonoBehaviour
{
    public Image[] noteImages;
    public ShakeScreen screenShake;

    int currentIndex = 0;

    Vector3 defaultScale = new Vector3(2f, 5.5f, 1f);
    public void OnCorrectInput()
    {
        if (currentIndex < noteImages.Length)
        {
            StartCoroutine(HideNoteWithEffect(noteImages[currentIndex]));
            currentIndex++;
        }
    }

    public void OnFail()
    {
        currentIndex = 0;
        if (screenShake != null)
        {
            screenShake.Shake();
        }

        for (int i = 0; i < noteImages.Length; i++)
        {
            noteImages[i].gameObject.SetActive(true);
            noteImages[i].transform.localScale = defaultScale;
        }
    }

    public IEnumerator HideNoteWithEffect(Image note)
    {
        float duration = 0.15f;
        float time = 0f;

        Vector3 startScale = defaultScale; 
        note.transform.localScale = startScale;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            note.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);

            yield return null;
        }

        note.gameObject.SetActive(false);
    }
}