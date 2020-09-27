using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextClass : MonoBehaviour
{
    public TextMesh bombNumber;
    Camera cameraToLookAt;

    // Use this for initialization 
    void Start()
    {
        cameraToLookAt = Camera.main;
        bombNumber.color = Color.black;
    }

    public void setText(string str) {
        bombNumber.text = str;
     
    }

    // Update is called once per frame 
    void LateUpdate()
    {
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }
}
