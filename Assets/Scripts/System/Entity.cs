using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Entity : MonoBehaviour
{

    public EntityHolder holder;
    public int currHunger;
    public string baseName;
    public GameObject body;

    private bool searching;

    private void Start()
    {
        Init();
    }

    public event Action<string> OnEntityDied;

    //Healthy Enough for Reproduction
    public bool isHealthy
    {
        get
        {
            return (!holder.profile.consumer || currHunger < holder.profile.maxHungerAllowed);
        }
    }

    public void Init() 
    {

        searching = false;

        holder.system.OnTick += OnTickEvent;
        OnEntityDied += holder.system.OnEntityDeathListener;

        name = holder.profile.name + holder.createdCounter;
        
        ++holder.createdCounter;
        ++holder.currCounter;

        if(holder.profile.consumer)
            setName();

        body.transform.SetParent(transform);
        transform.SetParent(holder.transform);
        //body.AddComponent(typeof(Entity)) as Entity;
    }

    void setName() 
    {
        name = baseName + " (" + currHunger + '/' + holder.profile.maxHungerBeforeDeath + ')';
    }

 

    protected void checkForMultiply() 
    {
        if (isHealthy) 
        {
            //eventually check for nearby radius of bunnies, but for now just gonna base it off totals
        }    
    }

    protected void checkForFood() 
    {
        if (currHunger >= holder.profile.hungerThreshold) 
        {
            if (!searching)
            {
                body.SendMessage("startSearching");
                searching = true;
            }
        }
    }

    private void OnTickEvent() 
    {

        checkForMultiply();
        //Debug.Log("Tick from" + name);
        if (holder.profile.objectType == ObjectType.Plant)
            return;

            currHunger += holder.profile.hungerAccumulationVal;

            if (currHunger >= holder.profile.maxHungerBeforeDeath)
            {
                Destroy(gameObject);
            }
            //Add running from predator
            else
            {
                checkForFood();
            }
            setName(); //resets name with huuger
       
        
    }

    private void OnDestroy()
    {
        --holder.currCounter;
        Destroy(body);
        OnEntityDied?.Invoke(name);
        holder.system.OnTick -= OnTickEvent; //UnSub
    }


    public void Eat(int nutritionalValue)
    {
        
        currHunger -= nutritionalValue;
        
        if (currHunger < 0)
            currHunger = 0;

        searching = false;
    }
}
