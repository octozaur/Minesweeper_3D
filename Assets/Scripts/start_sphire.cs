using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_sphire : MonoBehaviour
{


    private void OnMouseDown()
    {
        FindObjectOfType<StartScript>().GameProcess(2, 2, 2);
    }
}
