using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewNoteUIController : MonoBehaviour
{
    public Image[] noteImages;

    public Sprite A, S, D, F, G, H, J, K;

    public float startX = 100f;
    public float spacing = 150f;

    [SerializeField] Vector3 defaultScale = new Vector3(1.8f, 5f, 1f);
    public ShakeScreen screenShake;

    
    public void SetupNotes(KeyCode[] pattern)
    {
        ResetUI();
        ArrangeNotes(pattern.Length);

        for (int i = 0; i < noteImages.Length; i++)
        {
            if (i < pattern.Length)
            {
                noteImages[i].gameObject.SetActive(true);
                noteImages[i].transform.localScale = defaultScale;
                noteImages[i].sprite = GetSpriteFromKey(pattern[i]);
            }
            else
            {
                noteImages[i].gameObject.SetActive(false);
            }
        }
    }

    public void ArrangeNotes(int count)
    {
        for (int i = 0; i < noteImages.Length; i++)
        {
            if (i < count)
            {
                RectTransform rt = noteImages[i].rectTransform;

                float x = startX + (i * spacing);
                rt.anchoredPosition = new Vector2(x, 0f);
            }
        }
    }

    public void HideNoteAtIndex(int index)
    {
        if (index >= 0 && index < noteImages.Length)
        {
            noteImages[index].gameObject.SetActive(false);
        }
    }

    public void ResetUI()
    {
        for (int i = 0; i < noteImages.Length; i++)
        {
            noteImages[i].sprite = null; 
            noteImages[i].gameObject.SetActive(false);
        }
    }

    public void OnFail()
    {
        
        ResetUI();
        if (screenShake != null)
        {
            screenShake.Shake(); 
        }
    }

    public Sprite GetSpriteFromKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.A: return A;
            case KeyCode.S: return S;
            case KeyCode.D: return D;
            case KeyCode.F: return F;
            case KeyCode.G: return G;
            case KeyCode.H: return H;
            case KeyCode.J: return J;
            case KeyCode.K: return K;
        }
        return null;
    }
}