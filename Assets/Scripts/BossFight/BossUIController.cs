using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIController : MonoBehaviour
{
    public Image[] noteImages;

    public Sprite A, S, D, F, G, H, J, K;

    public void ShowNotes(KeyCode[] pattern)
    {
        for (int i = 0; i < noteImages.Length; i++)
        {
            if (i < pattern.Length)
            {
                noteImages[i].gameObject.SetActive(true);
                noteImages[i].transform.localScale = new Vector3(2f, 5.5f, 1f);
                noteImages[i].sprite = GetSprite(pattern[i]);
            }
            else
            {
                noteImages[i].gameObject.SetActive(false);
            }
        }
    }

    Sprite GetSprite(KeyCode key)
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