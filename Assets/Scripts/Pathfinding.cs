using UnityEngine;
using UnityEngine.AI;


public class Pathfinding : MonoBehaviour
{

    public Transform[] points;
    private NavMeshAgent nav;
    private int destPoint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!nav.pathPending && nav.remainingDistance < 0.5f)
            GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }
        nav.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;

    }
}