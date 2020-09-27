using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumColors : MonoBehaviour
{
    public Color[] colors = {
        Color.blue,
        Color.yellow,
        Color.green,
        Color.magenta,
        Color.gray,
        Color.black,
        Color.blue,
        Color.blue,
        Color.green,
        Color.magenta,
        Color.gray,
        Color.blue,
        Color.blue,
        Color.green,
        Color.magenta,
        Color.gray,
        Color.blue,
        Color.blue,
        Color.green,
        Color.magenta,
        Color.gray,
    };

    private void Start()
    {
        colors = new Color[10];
        colors[0] = Color.blue;
        colors[1] = Color.blue;
        colors[2] = Color.green;
        colors[3] = Color.magenta;
        colors[4] = Color.gray;
        colors[5] = Color.black;
        colors[6] = Color.blue;
        colors[7] = Color.blue;
        colors[8] = Color.blue;
        colors[9] = Color.blue;

    }
}
