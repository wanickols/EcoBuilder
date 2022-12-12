using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

public class EcoSystem : MonoBehaviour
{

    //Events
    public event Action OnTick; //Tick event //May pass number of tick later

    [Header("System Settings")]
    [SerializeField] private float tickDurationInSec = 1.0f;
    [SerializeField] public EntityProfile grassProfile, bunnyProfile, foxProfile;
    [SerializeField] public NetworkVariable<EntityHolder> grassHolder, bunnyHolder, foxHolder = new NetworkVariable<EntityHolder>(); //holders for entities
    [SerializeField] public int maxCount;

    [Header("Session Settings")]
    [SerializeField] private int startingGrass;
    [SerializeField] private int startingBunny;
    [SerializeField] private int startingFox;

    [Header("Session Data")]
    [SerializeField] private int numOfTicksInSession = 0;
    [SerializeField] public int currGrassCount, currBunnyCount, currFoxCount;


    [Header("Ecosystem Dimensions")]
    [SerializeField] public int xMax;
    [SerializeField] public int xMin, yMax, yMin, zMax, zMin;

    [SerializeField] public GetPoints points;

    // Start is called before the first frame update
    void Start()
    {
        initEcosystem();
        StartCoroutine(runGameLoop());
    }

    void initEcosystem()
    {
        //Holder init
        grassHolder.Value.Init("Grass", this);
        bunnyHolder.Value.Init("Bunnies", this);
        foxHolder.Value.Init("Foxes", this);

        //Create entities
        for (int i = 0; i < startingGrass; i++)
        {
            createEntity(grassHolder.Value);
        }

        for (int i = 0; i < startingBunny; i++)
        {
            createEntity(bunnyHolder.Value);
        }

        for (int i = 0; i < startingFox; i++)
        {
            createEntity(foxHolder.Value);
        }
        updateCounts();
    }

    IEnumerator runGameLoop()
    {
        while (true)
        {
            ++numOfTicksInSession;
            OnTick?.Invoke(); //Tick Event
            yield return new WaitForSeconds(tickDurationInSec);
        }
    }

    public void createEntity(EntityHolder holder)
    {
        updateCounts();
        if (holder.currCounter < holder.maxCounter)
        {
            GameObject body = Instantiate(holder.spawnObject, holder.getSpawnLocation(), Quaternion.identity) as GameObject;
            //Entity newEntity = body.AddComponent(typeof(Entity)) as Entity;
            //newEntity.Init(holder, body);
       
        }
        
    }




    //Will distribute this in final version
 

    public void OnEntityDeathListener(String name)
    {
        updateCounts();
    }
    
    public void updateCounts() 
    {
        currGrassCount = grassHolder.Value.currCounter;
        currBunnyCount = bunnyHolder.Value.currCounter;
        currFoxCount = foxHolder.Value.currCounter;
    }
   

}
