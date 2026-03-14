using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoControl : MonoBehaviour
{
    public ChangeCamera camera;
    public GameObject piano1;
    public GameObject piano2;
    public GameObject pianoControl;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.Checked == true)
        {
            Destroy(piano1);
            piano2.SetActive(true);
            Destroy(pianoControl, 0.5f);
        }
    }
}
