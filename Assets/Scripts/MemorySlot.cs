using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySlot : MonoBehaviour
{
    public bool isActive = false;

    private bool _isHidden = false;
    
    [SerializeField] private SpatialAnchorManager _spatialAnchorManager;
    [SerializeField] private GameObject _memorySlotBubble;
    [SerializeField] private GameObject _unpickedMemorySlotBubble;
    [SerializeField] private GameObject _invisibleMemorySlotBubble;
    [SerializeField] private MemoryBubbleManager _memoryBubbleManager;
    private void Start()
    {
        _memorySlotBubble.SetActive(false);
        _unpickedMemorySlotBubble.SetActive(true);
        isActive = false;
    }
    public void SetActive()
    {
        if (_isHidden) { return;}
        isActive = true;
        _memorySlotBubble.SetActive(true);
        _unpickedMemorySlotBubble.SetActive(false);
        //add to the list of active memory slots
        _memoryBubbleManager.AddMemorySlot(this);
        
    }
    
    public void SetInvisible()
    {
        if (_isHidden) { return;}
        isActive = false;
        _memorySlotBubble.SetActive(false);
        _unpickedMemorySlotBubble.SetActive(false);
        _invisibleMemorySlotBubble.SetActive(true);
    }
    
    public void HideAllUnusedMemorySlots()
    {
        if (!isActive)
        {
            _memorySlotBubble.SetActive(false);
            _unpickedMemorySlotBubble.SetActive(false);
            _invisibleMemorySlotBubble.SetActive(false);
            
        }
        
    }

    public void SaveAnchorSpot()
    {
        //use this location to save as a persistant spatial anchor.
        _spatialAnchorManager.CreateSpatialAnchor(transform.position, transform.rotation);
        SetInvisible();
        //should hide or delete the memory slot bubble.   
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
