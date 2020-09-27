using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
    public GameObject winLoose;
    public Text winLooseMess;
    public Text bombsRem;
    public int bombsRemInt;
    public int field_x = 5;
    public int field_y = 5;
    public int field_z = 5;
    public int step = 0;
    int step_num = 0;
    public int score = 0;
    public int bomb_pr = 90;
    public int bombs_on = 0;
    bool isEnd = false;
    GameObject[,,] texts;
    public bool bonus1;
    public int[,,] field;
    public Cell_3d[,,] object_field;
    public Mine_3d[,,] mine_field;
    public Cell_3d cell;
    public Mine_3d mine;
    public GameObject cube;
    public GameObject number;
    Camera cameraToLookAt;
    public GameObject cheetButton;
    public Color[] colors = {
        Color.blue,
        Color.yellow,
        Color.green,
        Color.magenta,
        Color.gray,
    };

    int dis_count = 0;

    void Start()
    {
        winLoose.SetActive(false);
        
        bombs_on = 0;
            bonus1 = SettingsControl.instance.cheet;
            field_x = SettingsControl.instance.x;
            field_y = SettingsControl.instance.y;
            field_z = SettingsControl.instance.z;
            bomb_pr = SettingsControl.instance.pr;
        if(bonus1 == false)
        {
            cheetButton.SetActive(false);
        }
        
        cameraToLookAt = Camera.main;
        field = new int[field_x, field_y, field_z];
        object_field = new Cell_3d[field_x, field_y, field_z];
        mine_field = new Mine_3d[field_x, field_y, field_z];
        texts = new GameObject[field_x, field_y, field_z];
        cube.transform.localScale = new Vector3(0, 0, 0);
        //cube.transform.localScale = new Vector3(1, 1, 1);
        for (int i = 0; i < field_x; i++)
        {
            for(int j = 0; j < field_y ; j++)
            {
                for(int k = 0; k < field_z; k++)
                {
                    field[i, j, k] = 0;
                    object_field[i, j, k] = Instantiate(cell, new Vector3(k*1.5f - (field_x - 1) * 0.75f, j*1.5f - (field_y - 1) * 0.75f, i*1.5f - (field_z - 1) * 0.75f), Quaternion.identity);
                    texts[i, j, k] = Instantiate(number, new Vector3(k * 1.5f - (field_x - 1) * 0.75f, j * 1.5f - (field_y - 1) * 0.75f, i * 1.5f - (field_z - 1) * 0.75f), Quaternion.identity);
                    mine_field[i, j, k] = Instantiate(mine, new Vector3(k * 1.5f - (field_x - 1) * 0.75f, j * 1.5f - (field_y - 1) * 0.75f, i * 1.5f - (field_z - 1) * 0.75f), Quaternion.identity);
                    texts[i, j, k].active = false;
                    mine_field[i, j, k].setUnactive();
                    //object_field[i, j, k].transform.SetParent(cube.transform);
                    //object_field[i, j, k].transform.parent = cube.transform;
                    object_field[i, j, k].x = i;
                    object_field[i, j, k].y = j;
                    object_field[i, j, k].z = k;
                }
            }
        }

    }

    public void setP() {
        cube.transform.localScale = new Vector3(1.5f * field_x - step, 1.5f * field_y - step, 1.5f * field_z - step);
        for (int i = 0; i < field_x; i++)
        {
            for (int j = 0; j < field_y; j++)
            {
                for (int k = 0; k < field_z; k++)
                {

                    object_field[i, j, k].transform.parent = cube.transform;
                    texts[i, j, k].transform.parent = cube.transform;
                    mine_field[i, j, k].transform.parent = cube.transform;

                }
            }
        }
    }

    public void unSetP()
    {
        
        for (int i = 0; i < field_x; i++)
        {
            for (int j = 0; j < field_y; j++)
            {
                for (int k = 0; k < field_z; k++)
                {
                    object_field[i, j, k].transform.parent = null;
                    texts[i, j, k].transform.parent = null;
                    mine_field[i, j, k].transform.parent = null;

                }
            }
        }
        cube.transform.localScale = new Vector3(0, 0, 0);
    }

    

    private void Update()
    {
        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * 30f;
        fov = Mathf.Clamp(fov, 15f, 90f);
        Camera.main.fieldOfView = fov;
        /*
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            //DisableBlocks();

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            //EnableBlocks();
        }
        */
    }
    
    public void GameProcess(int x, int y, int z)
    {
        if(isEnd == false)
        {
            if (step_num == 0)
            {
                FillField(x, y, z);
                step_num++;
            }
            else
            {
                CellProc(x, y, z);
                Debug.Log(x + " " + y + " " + z);
                //OpenCell(object_field[x, y, z]);
                step_num++;
                winCheck();
            }
        }
        
    }

    void winCheck() {
        int count = 0;
        for (int i = 0; i < field_x; i++)
        {
            for (int j = 0; j < field_y; j++)
            {
                for (int k = 0; k < field_z; k++)
                {
                    if (object_field[i, j, k].is_open == false)
                        count++;
                }
            }
        }
        if (count == bombs_on)
        {
            isEnd = true;
            GetComponent<UpBar>().isEnd = false;
            winLoose.SetActive(true);
            winLooseMess.text = "YOU WON";
            GetComponent<rotation>().isRot = false;
            
            Debug.Log("You Win!!!");
        }
            
    }

    void FillField(int x, int y, int z)
    {
        int xn = 0;
        int yn = 0;

        for (int i = 0; i < field_x; i++)
        {
            for(int j = 0; j < field_y; j++)
            {
                for(int k = 0; k < field_z; k++)
                {
                    if(ExepCheck(x, y, z, i, j, k, 3))
                    {
                        if(Random.Range(1, 100) < bomb_pr)
                        {
                            bombs_on++;
                            
                            //object_field[i, j, k].setMaterial(1);
                            int n = Random.Range(1, 100);
                            if (n < 30)
                            {
                                field[i, j, k] = -1;
                            }
                            else if (n > 30 && n < 60)
                            {
                                field[i, j, k] = -1;
                            }
                            else if (n > 60)
                            {
                                field[i, j, k] = -1;
                            }

                            for (int s = -1; s < 2; s++)
                            {
                                for (int m = -1; m < 2; m++)
                                {
                                    for (int d = -1; d < 2; d++)
                                    {
                                        
                                            if (i + s < field_x && i + s >= 0 && j + m < field_y && j + m >= 0 && k + d < field_z && k + d >= 0 && field[i + s, j + m, k + d] != -1)
                                            {
                                                field[i + s, j + m, k + d]++;

                                            }
                                       
                                    }
                                }
                            }
                            
                        }
                    }
                }
            }
        }
        bombsRemInt = bombs_on;
        bombsRem.text = bombsRemInt.ToString();
        if (bombsRemInt == 0)
        {
            GetComponent<UpBar>().isEnd = false;
            winLoose.SetActive(true);
            winLooseMess.text = "YOU WON";
        }
            
        CellProc(x, y, z);
    }

    public void setBombsCount(bool t) {
        if(t == true)
        {
            bombsRemInt--;
            bombsRem.text = bombsRemInt.ToString();
        }
        else
        {
            bombsRemInt++;
            bombsRem.text = bombsRemInt.ToString();
        }
        
    }

    void bombRadius(int x, int y, int z)
    {
        for (int i = 0; i < 3; i++) {
            for(int j = 0; j < 3; j++)
            {
                for(int k = 0; k < 3; k++)
                {
                    if (x - 1 + i < field_x && x - 1 + i >= 0 && y - 1 + j < field_y && y - 1 + j >= 0 && z - 1 + k < field_z && z - 1 + k >= 0)
                    {
                        if (field[x - 1 + i, y - 1 + j, z - 1 + k] < 0)
                        {
                            field[x - 1 + i, y - 1 + j, z - 1 + k]++;
                        }
                    }
                }
            }
        }
    }

    public void bonuses() {
        var t = true;
        for (int i = 0; i < field_x; i++)
        {
            if (t == false)
                break;
            for (int j = 0; j < field_y; j++)
            {
                if (t == false)
                    break;
                for (int k = 0; k < field_z; k++)
                {
                    if (field[i, j, k] >= 0 && object_field[i, j, k].is_flaged != true && object_field[i, j, k].is_open != true)
                    {
                        object_field[i, j, k].setMaterial(2);
                        t = false;
                        break;
                    }
                }
            }
        }
    }

    void BombAround(int x, int y, int z)
    {
        
        int n = Random.Range(1, 100);
        if (n < 30)
        {
            field[x, y, z] = -2;
        }
        else if( n > 30 && n < 60)
        {
            field[x, y, z] = -2;
        }
        else if (n > 60)
        {
            field[x, y, z] = -2;
        }

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                for (int k = -1; k < 2; k++)
                {
                    if (x + i != x && y + j != y && z + k != z)
                    {
                        
                        if (x + i < field_x && x + i >= 0 && y + j < field_y && y + j >= 0 && z + k < field_z && z + k >= 0 && field[x + i, y + j, z + k] != -1)
                        {
                            field[x + i, y + j, z + k]++;
                            Debug.Log((x + i)+" : "+(y + j)+" : "+(z + k)+" : "+ field[x + i, y + j, z  + k]);
                        }
                    }
                }
            }
        }

    }

    bool ExepCheck(int x0, int y0, int z0, int x, int y, int z, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                for (int k = 0; k < n; k++)
                {
                    if(x0 - 1 + i == x && y0 - 1 + j == y && z0 - 1 + k == z)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    void CellProc(int x, int y, int z)
    {
        Queue<Cell_3d> queue = new Queue<Cell_3d>();
        queue.Enqueue(object_field[x, y, z]);
        if (object_field[x, y, z].is_flaged == false)
        {
            while (queue.Count > 0)
            {

                Cell_3d current_field = queue.Dequeue();
                if (current_field.x < field_x && current_field.x >= 0 && current_field.y < field_y && current_field.y >= 0 && current_field.z < field_z && current_field.z >= 0)
                {
                    if (current_field.is_open == false)
                    {
                        OpenCell(current_field);
                        if (field[current_field.x, current_field.y, current_field.z] == 0)
                        {
                            for (int i = -1; i < 2; i++)
                            {
                                for (int j = -1; j < 2; j++)
                                {
                                    for (int k = -1; k < 2; k++)
                                    {
                                        if (current_field.x + i < field_x && current_field.x + i >= 0 && current_field.y + j < field_y && current_field.y + j >= 0 && current_field.z + k < field_z && current_field.z + k >= 0)
                                        {
                                            if (object_field[current_field.x + i, current_field.y + j, current_field.z + k].is_open == false && field[current_field.x + i, current_field.y + j, current_field.z + k] >= 0)
                                            {
                                                queue.Enqueue(object_field[current_field.x + i, current_field.y + j, current_field.z + k]);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (field[current_field.x, current_field.y, current_field.z] < 0)
                        {
                            if(field[current_field.x, current_field.y, current_field.z] == -1)
                            {

                            }
                            else if(field[current_field.x, current_field.y, current_field.z] == -2)
                            {
                                bombTwo(current_field.x, current_field.y, current_field.z);
                            }
                            else if (field[current_field.x, current_field.y, current_field.z] == -3)
                            {
                                bombTree(current_field.x, current_field.y, current_field.z);
                            }
                            isEnd = true;
                            GetComponent<UpBar>().isEnd = false;
                            
                            GetComponent<rotation>().isRot = false;
                            
                            Debug.Log("Game Over!");
                            //Application.LoadLevel(Application.loadedLevel);

                        }
                        current_field.is_open = true;

                    }
                }
            }
        }
    }

    IEnumerator bombsOpen(float time)
    {
        for (int i = 0; i < field_x; i++)
        {
            for (int j = 0; j < field_y; j++)
            {
                for (int k = 0; k < field_z; k++)
                {
                    if (field[i, j, k] < 0)
                    {
                        yield return new WaitForSeconds(time);
                        mine_field[i, j, k].setActive();
                        object_field[i, j, k].setUnactive();

                    }
                }
            }
        }
        winLoose.SetActive(true);
        winLooseMess.text = "YOU LOSE";

        // Code to execute after the delay
    }

    

    void bombTwo(int x, int y, int z) {
        Queue<Cell_3d> queue = new Queue<Cell_3d>();
        
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                for (int k = -1; k < 2; k++)
                {
                    if (x + i <= field_x && x + i >= 0 && y + j <= field_y && y + j >= 0 && z + k <= field_z + 1 && z + k >= 0)
                    {
                        queue.Enqueue(object_field[x + i, y + j, z + k]);
                        object_field[x + i, y + j, z + k].render.sharedMaterial = object_field[x + i, y + j, z + k].mat_01;
                        
                    }
                }
            }
        }
        while (queue.Count > 0)
        {

            Cell_3d current_field = queue.Dequeue();
            CellProc(current_field.x, current_field.y, current_field.z);

        }
    }

    void bombTree(int x, int y, int z)
    {

    }

    void OpenCell(Cell_3d current)
    {
        //GameObject tmp_numb = Instantiate(number, new Vector3(current.z * 1.5f - (field_z - 1) * 0.75f, current.y * 1.5f - (field_y - 1) * 0.75f, current.x * 1.5f - (field_x - 1) * 0.75f), Quaternion.identity);
        //tmp_numb.transform.parent = object_field[current.x, current.y, current.z].transform;
        texts[current.x, current.y, current.z].GetComponent<TextMesh>().text = field[current.x, current.y, current.z].ToString();
        if(field[current.x, current.y, current.z] >= 0)
        {
            texts[current.x, current.y, current.z].GetComponent<Renderer>().material.color = Color.yellow;
            if (texts[current.x, current.y, current.z].GetComponent<TextMesh>().text != "0")
                texts[current.x, current.y, current.z].SetActive(true);
        }
        else
        {
            mine_field[current.x, current.y, current.z].setActive();
            IEnumerator coroutine = bombsOpen(0.2f);
            StartCoroutine(coroutine);
            

        }
            
        object_field[current.x, current.y, current.z].setUnactive();
        //object_field[current.x, current.y, current.z].setMaterial(2);
    }

    private void DisableBlocks()
    {
        if(dis_count < field_z - 1)
        {
            for (int i = 0; i < field_x; i++)
            {
                for (int j = 0; j < field_y; j++)
                {
                    object_field[i, j, dis_count].render.enabled = false;
                }
            }
            dis_count++;
        }
    }

    private void EnableBlocks()
    {
        if (dis_count >= 0)
        {
            for (int i = 0; i < field_x; i++)
            {
                for (int j = 0; j < field_y; j++)
                {
                    object_field[i, j, dis_count].render.enabled = true;
                }
            }
            if(dis_count != 0)
                dis_count--;
        }
    }
}
