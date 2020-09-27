using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsControl : MonoBehaviour
{
    string dif;
    public int x;
    public int y;
    public int z;
    public int pr;
    public bool cheet = false;
    public GameObject inp1;
    public GameObject inp2;
    public GameObject inp3;
    public GameObject inp4;
    bool b4 = false;

    public static SettingsControl instance;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            Destroy(gameObject);
        }


        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }

    public void Start()
    {
        x = 5;
        y = 5;
        z = 5;
        pr = 10;
    }

    public void cheetCode()
    {
        if (cheet == false)
            cheet = true;
        else
            cheet = false;
    }

    public void Button1() {
        
        if (b4 == true)
        {

            inp1.SetActive(false);
            inp2.SetActive(false);
            inp3.SetActive(false);
            inp4.SetActive(false);
        }
        dif = "easy";
        x = 5;
        y = 5;
        z = 5;
        pr = 8;
        b4 = false;
        Debug.Log("easy");
    }
    public void Button2()
    {
        if (b4 == true)
        {

            inp1.SetActive(false);
            inp2.SetActive(false);
            inp3.SetActive(false);
            inp4.SetActive(false);
        }
        x = 7;
        y = 7;
        z = 7;
        pr = 9;
        b4 = false;
        dif = "easy";
        Debug.Log("Medium");
    }
    public void Button3()
    {
        if (b4 == true)
        {

            inp1.SetActive(false);
            inp2.SetActive(false);
            inp3.SetActive(false);
            inp4.SetActive(false);
        }
        x = 10;
        y = 10;
        z = 10;
        pr = 10;
        b4 = false;
        Debug.Log("Hard");
    }

    public void Inp1()
    {
        x = int.Parse(inp1.GetComponent<InputField>().text);
    }
    public void Inp2()
    {
        y = int.Parse(inp2.GetComponent<InputField>().text);
    }
    public void Inp3()
    {
        z = int.Parse(inp3.GetComponent<InputField>().text);
    }
    public void Inp4()
    {
        pr = int.Parse(inp4.GetComponent<InputField>().text);
    }

    public void Button4()
    {
        if(b4 == false)
        {
            inp1.SetActive(true);
            inp2.SetActive(true);
            inp3.SetActive(true);
            inp4.SetActive(true);
           
            b4 = true;
        }
        else if(b4 == true)
        {
            inp1.SetActive(false);
            inp2.SetActive(false);
            inp3.SetActive(false);
            inp4.SetActive(false);
            b4 = false;
        }
        
        Debug.Log("Easy");
    }

    public void ButtonStart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
