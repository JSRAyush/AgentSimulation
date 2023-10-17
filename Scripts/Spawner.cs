using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject parent;
    public int num;

    private int counter = 0;
    void Update()
    {     
        counter++;
        if (counter <= num)
        {
            Vector3 parentVec = parent.transform.position;
            Vector3 objectVec = objectToSpawn.transform.position;
            var newObject = Instantiate(objectToSpawn, objectVec, Quaternion.Euler(new Vector3()), parent.transform);   //Adding parent position and obj position for new spawn point (obj is relative to parent)
        }       

        if (parent.transform.childCount <= 2)
        {
            counter = 0;
        }
    }
}
