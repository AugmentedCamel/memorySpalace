using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BubbleEventLauncher : MonoBehaviour
{
    public UnityEvent bubbleEvent;
    
    public void LaunchBubbleEvent()
    {
        bubbleEvent.Invoke();
        Debug.Log("Bubble Event Launched");
    }
}
