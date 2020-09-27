using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BSettings : MonoBehaviour
{
    bool cool = false;

    public void chanState()
    {
        
        if (cool == false)
        {
            FindObjectOfType<StartScript>().bonuses();
            Debug.Log("hello");
            //Invoke("ResetCooldown", 10.0f);
            cool = false;
        }
        else
        {
            cool = true;
        }
    }

    public void restartState()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void menutState()
    {
        SceneManager.LoadScene(0);
    }

    public void settingstState()
    {
        SceneManager.LoadScene(1);
    }
}
