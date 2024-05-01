using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySlot : MonoBehaviour
{
    public bool isActive = false;
    [SerializeField] private GameObject _memorySlotBubble;
    [SerializeField] private GameObject _unpickedMemorySlotBubble;
    [SerializeField] private MemoryBubbleManager _memoryBubbleManager;
    private void Start()
    {
        _memorySlotBubble.SetActive(false);
        _unpickedMemorySlotBubble.SetActive(true);
        isActive = false;
    }
    public void SetActive()
    {
        isActive = true;
        _memorySlotBubble.SetActive(true);
        _unpickedMemorySlotBubble.SetActive(false);
        //add to the list of active memory slots
        _memoryBubbleManager.AddMemorySlot(this);
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
