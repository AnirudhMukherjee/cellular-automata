using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    int[,] map;

    public int width = 60;
    public int height = 60;

    [Range(0,100)]
    public int fillPercent;

    private void Start()
    {
        DrawMap();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DrawMap();
        }
    }

    void SmoothenMap()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j=0; j<height; j++)
            {
                int count = SurroundingCellCount(i, j);

                if (count > 4)
                {
                    map[i, j] = 1;
                }
                else
                {
                    map[i, j] = 0;
                }
            }
        }
    }

    int SurroundingCellCount(int x, int y)
    {
        int count = 0;
        for(int surrx = x - 1; surrx <= x + 1; surrx++)
        {
            for(int surry= y - 1; surry <= y + 1; surry++)
            {
                if (surrx >= 0 && surrx < width && surry >= 0 && surry < height)
                {
                    if (surrx != x || surry != y)
                    {
                        count += map[surrx, surry];
                    }
                }
                else
                {
                    count++;
                }
            }
        }
        return count;
    }

    void DrawMap()
    {
        map = new int[width, height];
        System.Random prng = new System.Random();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == width - 1)
                {
                    map[i, j] = 1;
                }
                else
                {
                    map[i, j] = (prng.Next(0, 100) > fillPercent) ? 1 : 0;
                }
            }
        }
        for (int i = 0; i < 5; i++)
        {
           SmoothenMap();
        }
    }

    private void OnDrawGizmos()
    {
        if (map != null)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Gizmos.color = (map[i, j] == 1) ? Color.white : Color.black;
                    Vector3 pos = new Vector3(-width / 2 + i + 0.5f, 0, -height / 2 + j + 0.5f);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }
}
