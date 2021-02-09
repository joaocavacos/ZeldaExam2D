using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    
    public SignalSender signal;
    public UnityEvent signalEvent;

    public void OnSignalRaised(){
        signalEvent.Invoke();
    }

    private void OnEnable() {
        signal.Register(this);
    }

    private void OnDisable() {
        signal.Unregister(this);
    }


}
