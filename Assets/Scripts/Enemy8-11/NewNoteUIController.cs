using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewNoteUIController : MonoBehaviour
{
    public Image[] noteImages;

    public int currentIndex = 0;



    [SerializeField] Vector3 defaultScale = new Vector3(1.8f, 5f, 1f);

    public Sprite spriteA;
    public Sprite spriteS;
    public Sprite spriteD;
    public Sprite spriteF;
    public Sprite spriteG;
    public Sprite spriteH;
    public Sprite spriteJ;
    public Sprite spriteK;



    public void HideNoteAtIndex(int index)
    {
        if (index >= 0 && index < noteImages.Length)
        {
            StartCoroutine(HideNoteWithEffect(noteImages[index]));
        }
    }


    public void OnFail()
    {
        StopAllCoroutines(); 

        currentIndex = 0;

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

    public Sprite GetSpriteFromKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.A: return spriteA;
            case KeyCode.S: return spriteS;
            case KeyCode.D: return spriteD;
            case KeyCode.F: return spriteF;
            case KeyCode.G: return spriteG;
            case KeyCode.H: return spriteH;
            case KeyCode.J: return spriteJ;
            case KeyCode.K: return spriteK;
            default: return spriteA;
        }
    }

    public void ResetUI()
    {
        StopAllCoroutines(); 

        currentIndex = 0;

        for (int i = 0; i < noteImages.Length; i++)
        {
            noteImages[i].gameObject.SetActive(true);
            noteImages[i].transform.localScale = defaultScale;
        }
    }


}