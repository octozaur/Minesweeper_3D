using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Cell_3d : MonoBehaviour
{
    public int x;
    public int y;
    public int z;

    public bool is_flaged = false;
    public bool is_open = false;
    public Material mat_01, mat_02;
    public Renderer render;
    public GameObject self;

    void Start()
    {
        render = GetComponent<Renderer>();
        render.enabled = true;
    }

    private void OnMouseOver()
    {
        Debug.Log("A");
        if (Input.GetMouseButtonDown(0)) {

            Debug.Log(x + " : " + y + " : " + z);
            FindObjectOfType<StartScript>().GameProcess(x, y, z);
        } else if (Input.GetMouseButtonDown(1))
        {
            if (is_flaged == false)
            {
                is_flaged = true;
                setMaterial(2);
                FindObjectOfType<StartScript>().setBombsCount(true);
            }
            else {
                is_flaged = false;
                setMaterial(1);
                FindObjectOfType<StartScript>().setBombsCount(false);
            }
        }


    }

    public void setUnactive() {
        self.active = false;
    }

    public void setMaterial(int i)
    {
        if (i == 1)
        {
            render.sharedMaterial = mat_01;
        }
        else if (i == 2)
        {
            render.sharedMaterial = mat_02;
        }
        
    }
}
