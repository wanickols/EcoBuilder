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
        fov = GetComponent<FieldOfView>();
        fov.OnTargetFound += Fov_OnTargetFound;
        fov.targetMask = LayerMask.GetMask("Food");
        agent = GetComponent<NavMeshAgent>();
        running = false;
        movementRadius = fov.viewRadius;
       
    }

    //When target found change transform;
    private void Fov_OnTargetFound()
    {
        Debug.Log("Found Target");
        target = fov.targetTransform;
        fov.OnTargetFound += Fov_OnTargetFound;
    }

    // Update is called once per frame
    protected void Update()
    {
            if (!target || target == null)
                idleMovement();
            else
            {
                agent.SetDestination(target.position);
            }
    }

    protected bool pathComplete()
    {
        if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }

        return false;
    }

    public void idleMovement() 
    {


        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
        {
            Debug.Log("I'm movin");
            agent.SetDestination(GetPoints.Instance.GetRandomPoint(transform, movementRadius));
        }
        else 
        {
            //Debug.Log(agent.steeringTarget);
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
