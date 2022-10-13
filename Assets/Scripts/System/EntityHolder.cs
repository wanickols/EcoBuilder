using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is just a holder class for animals of a certain type

[CreateAssetMenu(menuName = "SO/Entity Holder")]
public class EntityHolder : ScriptableObject
{

    [HideInInspector]
    public Transform transform;
    [HideInInspector]
    public int createdCounter, currCounter;
    [HideInInspector]
    public EcoSystem system;

    [Header("Entity Details")]
    public EntityProfile profile;
    public string entityName;


    public void Init(string name, EcoSystem system) 
    {
        transform = new GameObject(name).transform;
        this.system = system;
        transform.SetParent(system.transform);
    }



}
