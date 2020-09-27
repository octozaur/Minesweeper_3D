using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine_3d : MonoBehaviour
{
    public int x;
    public int y;
    public int z;

    
    public Renderer render;
    public GameObject self;

    void Start()
    {
        render = GetComponent<Renderer>();
        render.enabled = false;
    }

    public void setActive()
    {
        self.active = true;
    }

    public void setUnactive()
    {
        self.active = false;
    }
}
