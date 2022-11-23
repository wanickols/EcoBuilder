using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;

    public LayerMask targetMask;
    public Transform targetTransform;

    public event Action OnTargetFound;

    protected bool searching, running;

    private void Start()
    {
        startSearching();
    }

    //Public classes for messages
    public void startSearching() { searching = true; StartCoroutine(FindTargetWithDelay(.2f)); }

    public void checkMultiply(EntityProfile profile) 
    {
        LayerMask tempMask = LayerMask.GetMask(profile.name);
        Collider[] searchResults = getMultiplyCandidates(tempMask);

        //Debug.Log(profile.name + ": " + searchResults.Length + "/" + profile.parentCountRequired);
        if (searchResults.Length >= profile.parentCountRequired) 
        {
            FindClosestTarget(searchResults);
            Debug.Log(targetTransform);
        }
      
        // get closest
        //determine multiply
    }

    private Collider[] getMultiplyCandidates(LayerMask loverMask) 
    {
        return Physics.OverlapSphere(transform.position, viewRadius, loverMask);
 
    
    }

    //Coroutine
    IEnumerator FindTargetWithDelay(float delay) //searching for target
    {
        while (searching) 
        {
            //Debug.Log("Searching " + searching + viewRadius);
            yield return new WaitForSeconds(delay);
            if (FindClosestTarget()) 
            {
                OnTargetFound?.Invoke();
            }
        }
    }

    bool FindClosestTarget() 
    {
        //Finds all targets within radius
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (targetsInViewRadius.Length > 0)
        {
            float min = viewRadius;

            //Finds closest target
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (distToTarget < min)
                {
                    //if running run oposite direction //TODO

                    //Debug.Log("Searching Found");
                    searching = false;
                    targetTransform = target;
                    min = distToTarget;
                }
            }
          
            return true; //returns true if found
        }
        else 
        {
            //Debug.Log("No found target");
            return false; //returns false if nothing found
        }

    }

    bool FindClosestTarget(Collider[] targetsInViewRadius) 
    {
        if (targetsInViewRadius.Length > 0)
        {
            float min = viewRadius;

            //Finds closest target
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (distToTarget < min)
                {
                    //if running run oposite direction //TODO

                    //Debug.Log("Searching Found");
                    searching = false;
                    targetTransform = target;
                    min = distToTarget;
                }
            }

            return true; //returns true if found
        }
        else
        {
            //Debug.Log("No found target");
            return false; //returns false if nothing found
        }
    }

    public void stopSearching() { searching = false; }
    public void startRunning() { running = true; }
    public void stopRunning() { running = false; }
}
