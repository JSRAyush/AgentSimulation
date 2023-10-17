using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Setting up the grid in Unity environment. Separate from the Grid class.
public class GridTest : MonoBehaviour   
{
    private Transform gridOrigin;
    public int xNum;
    public int zNum;
    public float cellSize;
    public GameObject parentAgent;
    public int updateBuffer;

    private List<Vector3> objectPos;
    private int bufferCounter;

    private Grid grid;    
    private void Start()
    {        
        bufferCounter = 0;
        gridOrigin = GetComponent<Transform>();
        objectPos = new List<Vector3>();
        for (int i = 0; i < parentAgent.transform.childCount; i++)
        {
            var child = parentAgent.transform.GetChild(i);
            objectPos.Add(child.transform.position);
        }
        grid = new Grid(xNum, zNum, cellSize, gridOrigin.position);
    }

    private void Update()
    {       
        bufferCounter++;
        if (bufferCounter >= updateBuffer)
        {
            bufferCounter = 0;            
            objectPos.Clear();
            for (int i = 0; i < parentAgent.transform.childCount; i++)
            {
                var child = parentAgent.transform.GetChild(i);
                objectPos.Add(child.transform.position);
            }

            for (int i = 0; i < objectPos.Count; i++)
            {
                grid.ValueIncrement(objectPos[i]);
            }


            if(parentAgent.transform.childCount <= 2)
            {
                grid.ResetGrid();
            }
        }        
    }

    public Grid GetGrid()
    {
        return grid;
    }

}
