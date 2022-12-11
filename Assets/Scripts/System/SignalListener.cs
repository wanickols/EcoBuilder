using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{

    [SerializeField] private Signaler signal;
    [SerializeField] private UnityEvent signalEvent;
    public void OnSignalRaised() 
    {
        signalEvent.Invoke();
    }

    private void OnEnable()
    {
        signal.registerListener(this);
    }

    private void OnDisable()
    {
        signal.deregisterListener(this);
    }
}
