using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;   //Using external library to get TextMesh output on Gridcells


//Grid Class, tha would hold values for each cell.
public class Grid 
{
    private int xNum;
    private int zNum;
    private float cellSize;
    private Vector3 originPosition;

    private int[,] gridArray;
    private TextMesh[,] debugTextMesh;

    public Grid(int xNum, int zNum, float cellSize, Vector3 originPosition)
    {
        this.xNum = xNum;
        this.zNum = zNum;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        this.gridArray = new int[xNum, zNum];      
        this.debugTextMesh = new TextMesh[xNum, zNum];

        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                debugTextMesh[i,j] = UtilsClass.CreateWorldText(gridArray[i, j].ToString(), null, GetWorldPosition(i, j) + new Vector3(cellSize, 0, cellSize) * .5f, 5, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j + 1), Color.white, 100f); //Grid visualization
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i + 1, j), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, zNum), GetWorldPosition(xNum, zNum), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(xNum, 0), GetWorldPosition(xNum, zNum), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x , int z)  //get vector value from ints
    {
        return new Vector3(x, 0, z) * cellSize + originPosition;   //WorldXY = UnityXZ
    }
    private void GetXZ(Vector3 worldPosition, out int x , out int z)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize); 
    }
    public void SetValue(int x, int z , int value)
    {
        if(x>=0 && z>=0 && x<xNum && z < zNum)
        {
            gridArray[x, z] = value;
            debugTextMesh[x, z].text = gridArray[x, z].ToString();
        }        
    }

    public void SetValue(Vector3 worldPosition , int value)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        SetValue(x, z, value);
    }     
    
    public int GetValue(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < xNum && z < zNum)
        {
            return gridArray[x, z];
        }
        else return 0;
    }
    public int GetValue(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetValue(x, z);
    }

    public void ValueIncrement(Vector3 worldPosition)  //increase placeHolder value by 1
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        if (x >= 0 && z >= 0 && x < xNum && z < zNum)
        {
            gridArray[x, z] += 1;
            debugTextMesh[x, z].text = gridArray[x, z].ToString();
        }
    }

    public void GetDimensions(out int xSize, out int zSize)
    {
        xSize = xNum;
        zSize = zNum;
    }

    public void ResetGrid()
    {
        for (int i = 0; i < xNum; i++)
        {
            for (int j = 0; j < zNum; j++)
            {
                gridArray[i, j] = 0;
            }
        }
    }
}
