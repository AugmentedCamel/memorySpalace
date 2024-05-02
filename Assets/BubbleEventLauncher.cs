using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BubbleEventLauncher : MonoBehaviour
{
    public UnityEvent bubbleEvent;
    public UnityEvent bubbleGrabDataEvent;
    [SerializeField] private GrabReleaserAndReceiver _grabReleaserAndReceiver;
    
    public void LaunchBubbleEvent()
    {
        bubbleEvent.Invoke();
        Debug.Log("Bubble Event Launched");
    }
    
    public void LaunchBubbleGrabEvent(GameObject obj)
    {
        _grabReleaserAndReceiver.GrabTriggerRelease(obj);
        
    }
    
    
}
