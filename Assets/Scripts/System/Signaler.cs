using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Signaler : ScriptableObject
{
    [SerializeField] List<SignalListener> listeners = new List<SignalListener>();

    public void Raise()  
    {
        foreach (SignalListener sl in listeners) 
        {
            sl.OnSignalRaised();
        }
    }

    public void registerListener(SignalListener listener) 
    {
        listeners.Add(listener);
    }

    public void deregisterListener(SignalListener listener) 
    {
        listeners.Remove(listener);
    }
}
