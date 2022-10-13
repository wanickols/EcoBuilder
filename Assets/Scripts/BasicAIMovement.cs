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

    // Start is called before the first frame update
    protected void Start()
    {
        running = false;
        fov = GetComponent<FieldOfView>();
        fov.targetMask = LayerMask.GetMask("Food");

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
            Debug.Log("Not running! good. ");
            if (!target)
                idleMovement();
            else
                agent.SetDestination(target.position);
        }
        else 
        {
            Debug.Log("Running. Bad!");
        }
    }

    public void idleMovement() 
    {
        Debug.Log("Idling. Bad!");
        //move random direction lifelike
    }

    //Predator Event overides everything
}
