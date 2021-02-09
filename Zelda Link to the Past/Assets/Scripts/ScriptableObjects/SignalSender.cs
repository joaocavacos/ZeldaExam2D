using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SignalSender : ScriptableObject
{
    //Signal to send, observer pattern - player sends signal to hearts
    public List<SignalListener> listeners = new List<SignalListener>();

    public void Raise(){

        for (int i = listeners.Count - 1 ; i >= 0; i--)
        {
            listeners[i].OnSignalRaised();
        }
    }

    public void Register(SignalListener listener){
        listeners.Add(listener);
    }

    public void Unregister(SignalListener listener){
        listeners.Remove(listener);
    }

}
