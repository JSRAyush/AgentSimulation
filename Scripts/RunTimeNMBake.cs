using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunTimeNMBake : MonoBehaviour
{
    public NavMeshSurface[] surfaces;   //Uses NavMeshComponents package.  "https://github.com/Unity-Technologies/NavMeshComponents"
    public int frameCounter;   //to rebuild NavMesh after n frames

    private int counter;

    void Start()
    {
        counter = 0;
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }  
    }

    void Update()
    {
        counter++;
        if(counter > frameCounter)
        {
            //Debug.Log("Rebuilding NavMesh");
            Start();
        }
    }
}
