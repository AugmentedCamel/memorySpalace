using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryBubbleManager : MonoBehaviour
{
    public List<MemorySlot> memorySlots;
    
    // Start is called before the first frame update
    public void AddMemorySlot(MemorySlot memorySlot)
    {
        memorySlots.Add(memorySlot);
    }
    
    public bool CheckOrderMemorySlots(MemorySlot memorySlot, int index)
    {
        if (memorySlots[index] == memorySlot)
        {
            return true;
        }
        return false;
    }
    
    public void StartTrainingData()
    {
        foreach (var memorySlot in memorySlots)
        {
            memorySlot.SetInvisible();
        }
       
    }

    public void HideAllUnusedAnchors()
    {
        MemorySlot[] AllMemorySlots = FindObjectsOfType<MemorySlot>();
        foreach (var memorySlot in AllMemorySlots)
        {
            memorySlot.HideAllUnusedMemorySlots();
        }
        
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
