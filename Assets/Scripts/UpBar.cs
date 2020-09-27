using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpBar : MonoBehaviour
{
    public Text text;
    public float d;
    public bool isEnd = true;

    private void Update()
    {
        if (isEnd)
        {
            d += Time.deltaTime;
            text.text = d.ToString("F1");
        }
        
    }
}
