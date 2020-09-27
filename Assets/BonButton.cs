using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonButton : MonoBehaviour
{
    bool cool = false;

    public void chanState()
    {
        if (cool == false) {
            FindObjectOfType<StartScript>().bonuses();
            Debug.Log("hello");
            Invoke("ResetCooldown", 10.0f);
            cool = false;
        }
        else {
            cool = true;
        }
    }
}
