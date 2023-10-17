using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DomAgentSim : MonoBehaviour
{
    public List<Goal> goals = new List<Goal>();
    
    private NavMeshAgent m_agent;
    private Transform m_pos;
    private List<Transform> currGoals;
    private int counter;
    private double size;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        m_agent = GetComponent<NavMeshAgent>();
        m_pos = GetComponent<Transform>();
        size = GetComponent<CapsuleCollider>().radius;

        if (m_agent.gameObject.name.Contains("(Clone)"))
        {
            m_agent.enabled = true;
        }
        else m_agent.enabled = false;
        currGoals = goals[0].m_goals;        
        m_agent.SetDestination(currGoals[SetGoalIdx(currGoals)].position);    //Goal selected from the list of Level 1 targets
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = m_pos.position - m_agent.destination;  //from current position to goal

        if (vec.magnitude<=(m_agent.stoppingDistance+ size * 1.1))
        {
            //Debug.Log(string.Format("moving to next stage, current stage:{0}",counter));
            counter++;
            if (counter >= goals.Count)
            {
                //Debug.Log("about to be destroyed");
                Destroy(m_agent.gameObject);
                return;
            }
            currGoals = goals[counter].m_goals;
            m_agent.SetDestination(currGoals[SetGoalIdx(currGoals)].position);  //set next distination if target achieved
        }        
    }

    int SetGoalIdx(List<Transform> currGoals)
    {
        int r = Random.Range(0, currGoals.Count - 1);
        return r;
    }
}

/// <summary>
/// Class to hold List<Transform>
/// </summary>

[System.Serializable]
public class Goal   
{
    public List<Transform> m_goals;
}
