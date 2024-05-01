using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySlot : MonoBehaviour
{
    public bool isActive = false;
    [SerializeField] private GameObject memorySlotBubble;
    [SerializeField] private GameObject unpickedMemorySlotBubble;
    
    private void Start()
    {
        memorySlotBubble.SetActive(false);
        unpickedMemorySlotBubble.SetActive(true);
        isActive = false;
    }
    public void SetActive()
    {
        isActive = true;
        memorySlotBubble.SetActive(true);
        unpickedMemorySlotBubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
