using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Entity Profile")]
public class EntityProfile : ScriptableObject
{
    public ObjectType objectType;
    public int nutritionalValue;

    [Header("Multiplyer")]
    public bool multiplier; //does this object multiply itself
    public LayerMask multiplyerMask;
    public int multiplyFrequency; 
    public int parentCountRequired; //How many opjects must be nearby to multiply
    public int maxHungerAllowed; //How hungry a object can be to multiply
    public int minChildCount;
    public int maxChildCount;

    [Header("Consumer")]
    public bool consumer;
    public int maxHungerBeforeDeath;
    public int hungerAccumulationVal;
    public int hungerThreshold;
    public ObjectType foodSource; //no omnivores rn

    
    public int getChildCount()
    {
        return Random.Range(minChildCount, maxChildCount);
    }


}

public enum ObjectType
{
    Plant,
    Animal
}