using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

public class EcoSystem : NetworkBehaviour
{

    //Events
    public event Action OnTick; //Tick event //May pass number of tick later

  

    [Header("System Settings")]
    [SerializeField] private float tickDurationInSec = 1.0f;
    [SerializeField] public EntityProfile grassProfile, bunnyProfile, foxProfile;
    [SerializeField] public EntityHolder grassHolder, bunnyHolder, foxHolder; // holders for entities
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
    private NetworkObject networkObject;

    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        if (!networkObject)
            networkObject = GetComponent<NetworkObject>();
        if (!IsSpawned)
            networkObject.Spawn();
        
        if (!IsOwner)
            return;
        Debug.Log("Ecoystem Init");

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
            GameObject body = Instantiate(holder.spawnObject, holder.getSpawnLocation(), Quaternion.identity);
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
        if (!IsOwner)
            return;

        currGrassCount = grassHolder.currCounter;
        currBunnyCount = bunnyHolder.currCounter;
        currFoxCount = foxHolder.currCounter;
    }
   

}
