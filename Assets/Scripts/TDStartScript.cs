
using System.Collections.Generic;
using UnityEngine;

public class TDStartScript : MonoBehaviour
{
    public int field_x = 5;
    public int field_y = 5;
    public int step = 0;

    int step_num = 0;
    public int bombs = 10;
    int[][] exept = new int[49][];
    bool cell_open;
    public int bomb_pr = 15;

    public int[,] field;
    public Cell_Id[,] object_field;
    public Cell_Id cell;
    public GameObject cube;
    public Camera camera;
    public GameObject number;
    
    
    // Start is called before the first frame update
    void Start()
    {


        field = new int[field_x, field_y];
        object_field = new Cell_Id[field_x, field_y];

        

        camera.transform.position = new Vector3(0, 0, -(field_x + field_y));
        cube.transform.localScale = new Vector3(1.5f * field_x - step, 1.5f * field_y - step, cell.transform.localScale.z);
        for (int i = 0; i < field_x; i++)
        {
            for (int j = 0; j < field_y; j++)
            {
                object_field[i, j] = Instantiate(cell, new Vector3(j * 1.5f - (field_y - 1) * 0.75f, i * 1.5f - (field_x - 1) * 0.75f, 0), Quaternion.identity);
                object_field[i, j].x_a = i;
                object_field[i, j].y_a = j;
                object_field[i, j].transform.SetParent(cube.transform);
            }
        }
    }

    
    public void RightProcess(int x, int y)
    {
        if(object_field[x, y].is_flaged == false)
        {
            object_field[x, y].setMaterial(3);
            object_field[x, y].is_flaged = true;
        }
        else
        {
            object_field[x, y].setMaterial(1);
            object_field[x, y].is_flaged = false;
        }
    }

    public void GameProcess(int x, int y)
    {
        if(step_num == 0)
        {
            FillField(x, y);
            step_num++;
        }
        else
        {
            CellProc(x, y);
            step_num++;
        }
    }

    public void FillField(int x, int y)
    {
        int bombs_on = 0;
        int xn = 0;
        int yn = 0;

        for (int i = 0; i < field_x; i++)
        {
            for (int j = 0; j < field_y; j++)
            {
                
                if (ExepCheck(x, y, i, j, 5))
                {
                    if (Random.Range(1, 100) < bomb_pr)
                    {
                        BombAround(i, j);
                        bombs_on++;
                    }
                }
            }
        }

        CellProc(x, y);
       
    }


    void OpenCell(Cell_Id current)
    {
        GameObject tmp_numb = Instantiate(number, new Vector3(current.y_a * 1.5f - (field_y - 1) * 0.75f, current.x_a * 1.5f - (field_x - 1) * 0.75f, 0), Quaternion.identity);
        tmp_numb.GetComponent<TextMesh>().text = field[current.x_a, current.y_a].ToString();
        object_field[current.x_a, current.y_a].setMaterial(2);
    }

    void CellProc(int x, int y)
    {
        Queue<Cell_Id> queue = new Queue<Cell_Id>();
        queue.Enqueue(object_field[x, y]);
        if(object_field[x, y].is_flaged == false) { 
        while(queue.Count > 0){

            Cell_Id current_field = queue.Dequeue();
            if(current_field.x_a < field_x && current_field.x_a >= 0 && current_field.y_a < field_y && current_field.y_a >= 0)
            {
                if(current_field.is_open == false)
                {
                    OpenCell(current_field);
                    if (field[current_field.x_a, current_field.y_a] == 0)
                    {
                        for(int i = -1; i < 2; i++)
                        {
                            for(int j = -1; j < 2; j++)
                            {
                                if(current_field.x_a + i < field_x && current_field.x_a + i >= 0 && current_field.y_a + j < field_y && current_field.y_a + j >= 0)
                                {
                                    if(object_field[current_field.x_a + i, current_field.y_a + j].is_open == false && field[current_field.x_a + i, current_field.y_a + j] >= 0)
                                    {
                                        queue.Enqueue(object_field[current_field.x_a + i, current_field.y_a + j]);
                                    }
                                }
                            }
                        }
                    }
                    else if(field[current_field.x_a, current_field.y_a] < 0)
                    {
                        Debug.Log("Game Over!");
                        PrintField();
                    }
                    current_field.is_open = true;
                    
                }
            }
        }
        }


    }


    void PrintField()
    {
        for (int i = 0; i < field_x; i++)
        {
            for (int j = 0; j < field_y; j++)
            {
                GameObject tmp_numb = Instantiate(number, new Vector3(j * 1.5f - (field_y - 1) * 0.75f, i * 1.5f - (field_x - 1) * 0.75f, 0), Quaternion.identity);
                tmp_numb.GetComponent<TextMesh>().text = field[i, j].ToString();
            }
        }
    }

    bool ExepCheck(int x0, int y0, int x, int y, int n)
    {
        int st = 1;
        if (x0 == x && y0 == y)
        {
            return false;
        }
        for (int i = 2; i < n; i = i + 2)
        {
            for(int j = 0; j < i; j++)
            {
                if((x0 - st + j) == x && (y0 - st) == y)
                {
                    return false;
                }
                if ((x0 + st) == x && (y0 - st + j) == y)
                {
                    return false;
                }
                if ((x0 + st - j) == x && (y0 + st) == y)
                {
                    return false;
                }
                if ((x0 - st) == x && (y0 + st - j) == y)
                {
                    return false;
                }
            }
            st++;
        }
        return true;
    }

    

    void BombAround(int x, int y)
    {
        field[x, y] = -1;
        if (y + 1 < field_y && field[x, y + 1] != -1)
            field[x, y + 1]++;
        if (x + 1 < field_x && y + 1 < field_y && field[x + 1, y + 1] != -1)
           field[x + 1, y + 1]++;
        if (x + 1 < field_x && field[x + 1, y] != -1)
            field[x + 1, y]++;
        if (y - 1 >= 0 && field[x, y - 1] != -1)
            field[x, y - 1]++;
        if (x - 1 >= 0 && y - 1 >= 0 && field[x - 1, y - 1] != -1)
            field[x - 1, y - 1]++;
        if (x - 1 >= 0 && field[x - 1, y] != -1)
            field[x - 1, y]++;
        if (x - 1 >= 0 && y + 1 < field_y && field[x - 1, y + 1] != -1)
            field[x - 1, y + 1]++;
        if (x + 1 < field_x && y - 1 >= 0 && field[x + 1, y - 1] != -1)
            field[x + 1, y - 1]++;
    }

    void FillExeptions(int x, int y)
    {
        
    }




}
