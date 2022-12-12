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
    public event Action OnMultiply;

    protected bool searching, running;

    private void Start()
    {
        startSearching();
    }

    //Public classes for messages
    public void startSearching() { searching = true; StartCoroutine(FindTargetWithDelay(.2f)); }

    public void checkMultiply(EntityProfile profile) 
    {

        List<Collider> searchResults = getMultiplyCandidates(profile.multiplyerMask);

        //Debug.Log(profile.name + ": " + (searchResults.Count + 1) + "/" + profile.parentCountRequired);
        if (searchResults.Count + 1 >= profile.parentCountRequired) 
        {
            FindClosestTarget(searchResults);
            //Debug.Log(targetTransform);
            OnMultiply?.Invoke();
          
        }
      
        // get closest
        //determine multiply
    }

    private List<Collider> getMultiplyCandidates(LayerMask loverMask)
    {
        Collider[] searchResults = Physics.OverlapSphere(transform.position, viewRadius, loverMask);
        List<Collider> filteredResults = new List<Collider>();
        int i = 0;
        foreach (var hitAsset in searchResults)
        {
            if (hitAsset.transform.position != transform.position)
            { //do something} }

                filteredResults.Add(hitAsset);
                i++;
            }
        }
        return filteredResults;
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
                    targetTransform = target;
                    min = distToTarget;
                    //Debug.Log("Searching Found");

                }
            }
            searching = false;
            return true; //returns true if found
        }
        else 
        {
            //Debug.Log("No found target");
            return false; //returns false if nothing found
        }

    }

    bool FindClosestTarget(List<Collider> targetsInViewRadius) 
    {
        if (targetsInViewRadius.Count > 0)
        {
            float min = viewRadius;

            //Finds closest target
            for (int i = 0; i < targetsInViewRadius.Count; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (distToTarget < min)
                {
                    //if running run oposite direction //TODO

                    //Debug.Log("Searching Found");
                   
                    targetTransform = target;
                    min = distToTarget;
                }
            }
            searching = false;
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
