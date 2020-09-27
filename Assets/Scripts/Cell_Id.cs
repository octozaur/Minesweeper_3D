
using UnityEngine;

public class Cell_Id : MonoBehaviour
{
    public int x_a = 0;
    public int y_a = 0;

    Renderer render;
    public Material mat_01, mat_02, mat_03;

    public bool is_open = false;
    public bool is_flaged = false;

    private void Start()
    {
        render = GetComponent<Renderer>();
        render.enabled = true;
        render.sharedMaterial = mat_01; 
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            FindObjectOfType<TDStartScript>().GameProcess(x_a, y_a);
        if (Input.GetMouseButtonDown(1))
        {
            FindObjectOfType<TDStartScript>().RightProcess(x_a, y_a);
        }
        

    }

    public void setMaterial(int i)
    {
        if(i == 1)
        {
            render.sharedMaterial = mat_01;
        }
        else if(i == 2)
        {
            render.sharedMaterial = mat_02;
        }
        else if(i == 3)
        {
            render.sharedMaterial = mat_03;
        }
    }

    
}
