using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;
    public bool Checked = false;
    public GameObject cameraControl;
    void Start()
    {
        
    }
   void OnTriggerEnter2D(Collider2D other)
   {
        if(other.CompareTag("Player"))
        {
            Checked = true;
        }
   }

    // Update is called once per frame
    void Update()
    {
        if (Checked == true)
        {
            Destroy(camera1);
            camera2.SetActive(true);
            Destroy(cameraControl, 0.5f);
        }
       
        

    }
}
