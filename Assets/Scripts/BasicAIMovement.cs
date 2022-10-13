using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAIMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform target;

    protected FieldOfView fov;

    protected bool running; //mot sure if this wil stay here

    public NavMeshAgent agent;
    public float movementRadius;


    // Start is called before the first frame update
    protected void Start()
    {
       
        agent = GetComponent<NavMeshAgent>();
        running = false;
        fov = GetComponent<FieldOfView>();
        fov.targetMask = LayerMask.GetMask("Food");
        movementRadius = fov.viewRadius;
        fov.onTargetFound += Fov_onTargetFound;
    }

    //When target found change transform;
    private void Fov_onTargetFound()
    {
        target = fov.targetTransform;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!running)
        {
            if (!target)
                idleMovement();
            else
            {
                agent.SetDestination(target.position);
            }
        }
        else 
        {
            //Debug.Log("Running. Bad!");
        }
    }

    public void idleMovement() 
    {

        if (!agent.hasPath)
        {
            Debug.Log("Idling");
            agent.SetDestination(GetPoints.Instance.GetRandomPoint(transform, movementRadius));
        }
        else 
        {
            Debug.Log(agent.steeringTarget);
        }
    }


    //Predator Event overides everything
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, movementRadius);
    }

#endif

}
