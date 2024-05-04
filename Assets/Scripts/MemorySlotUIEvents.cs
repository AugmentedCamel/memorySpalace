using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySlotUIEvents : MonoBehaviour
{
    [SerializeField] private GameObject _memorySpot;
    [SerializeField] private GameObject _emptyMemorySlot;
    [SerializeField] private GameObject _buttonOrderTest;
    
    // Start is called before the first frame update
    public void DebugLog()
    {
        Debug.Log("Memory Slot Clicked");
    }
    
    public void ActivateMemorySlot()
    {
        Debug.Log("Memory Slot Activated");
        _memorySpot.SetActive(true);
        _emptyMemorySlot.SetActive(false);
        
    }

    public void ActivateButtonOrderTest(bool state)
    {
        _buttonOrderTest.SetActive(state);
        
    }
    
    
    public void CheckOrderButton()
    {
        Debug.Log("Order Button Pressed");
    }
}
