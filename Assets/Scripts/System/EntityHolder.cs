using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is just a holder class for animals of a certain type

[CreateAssetMenu(menuName = "SO/Entity Holder")]
public class EntityHolder : ScriptableObject
{

    [HideInInspector]
    [SerializeField] public Transform transform;
    [HideInInspector]
    [SerializeField] public int createdCounter, currCounter;
    [HideInInspector]
    [SerializeField] public EcoSystem system;

    [Header("Entity Details")]
    [SerializeField] public EntityProfile profile;
    [SerializeField] public string entityName;
    [SerializeField] public int maxCounter;


    private int xPos, yPos, zPos;

    public GameObject spawnObject;

    public void Init(string name, EcoSystem system) 
    {
        transform = new GameObject(name).transform;
        this.system = system;
        currCounter = createdCounter = 0;
        maxCounter = system.maxCount;
        transform.SetParent(system.transform);
    }

    public Vector3 getSpawnLocation() 
    {
        xPos = Random.Range(system.xMin, system.xMax + 1);
        yPos = Random.Range(system.yMin, system.yMax + 1);
        zPos = Random.Range(system.zMin, system.zMax + 1);
        return new Vector3(xPos, yPos, zPos);
    }


}
