using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EcoSystem : MonoBehaviour
{

    //Events
    public event Action OnTick; //Tick event //May pass number of tick later

    [Header("System Settings")]
    public float tickDurationInSec = 1.0f;
    public EntityProfile grassProfile, bunnyProfile, foxProfile;
    public EntityHolder grassHolder, bunnyHolder, foxHolder; //folders objects for entities 

    [Header("Session Settings")]
    public int startingGrass;
    public int startingBunny;
    public int startingFox;

    [Header("Session Data")]
    public int numOfTicksInSession = 0;
    public int currGrassCount, currBunnyCount, currFoxCount;


    // Start is called before the first frame update
    void Start()
    {
        initEcosystem();
        StartCoroutine(runGameLoop());
    }

    void initEcosystem()
    {
        //Holder init
        grassHolder.Init("Grass", this);
        bunnyHolder.Init("Bunnies", this);
        foxHolder.Init("Foxes", this);

        //Create entities
        for (int i = 0; i < startingGrass; i++)
        {
            createEntity(grassHolder);
        }

        for (int i = 0; i < startingBunny; i++)
        {
            createEntity(bunnyHolder);
        }

        for (int i = 0; i < startingFox; i++)
        {
            createEntity(foxHolder);
        }
   
    }

    IEnumerator runGameLoop()
    {
        while (true)
        {

            checkForMultiply(grassHolder);
            checkForMultiply(bunnyHolder);
            checkForMultiply(foxHolder);

            ++numOfTicksInSession;
            OnTick?.Invoke(); //Tick Event
            yield return new WaitForSeconds(tickDurationInSec);
        }
    }

    void createEntity(EntityHolder holder)
    {
        Entity newEntity = new GameObject().AddComponent<Entity>();
        newEntity.Init(holder);
        updateCounts();
    }




    //Will distribute this in final version
    void checkForMultiply(EntityHolder holder)
    {
        if (!holder)
        {
            //Debug.Log("No Holder");
            return;
        }

        if ((numOfTicksInSession + 1) % holder.profile.multiplyFrequency != 0)
            return;

        int numHealthy = 0;
        foreach (Transform e in holder.transform)
        {
            if (e.GetComponent<Entity>().isHealthy)
            {
                ++numHealthy;
            }
        }

        int timesMultiply = numHealthy / holder.profile.parentCountRequired;
        for (int i = 0; i < timesMultiply; i++)
        {
            for (int j = 0; j < holder.profile.getChildCount(); j++)
            {
                createEntity(holder);
            }
        }
    }

    public void OnEntityDeathListener(String name)
    {
        updateCounts();
    }
    
    //Debug Purposes
    private void updateCounts() 
    {
        currGrassCount = grassHolder.currCounter;
        currBunnyCount = bunnyHolder.currCounter;
        currFoxCount = foxHolder.currCounter;
    }
   

}
