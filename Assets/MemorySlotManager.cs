using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySlotManager : MonoBehaviour
{
    [SerializeField] private AnchorspotManager _anchorspotManager;
    [SerializeField] private AnchorSaver _anchorSaver;
    public List<Anchorspot> memorySlots = new List<Anchorspot>(); //a list with all the memory slots
    
    
    public void OverrideMemory() //function to save memoryslots to memory
    {
        //_anchorSaver.SaveAnchorList(_anchorspotManager.anchorspots); //save to long term memory
        
        /*memorySlots.Clear();
        Debug.Log("Memory Overriden");
        foreach (Anchorspot anchorspot in _anchorspotManager.anchorspots)
        {
            if (anchorspot.IsChosen)
            {
                memorySlots.Add(anchorspot);
            }
        }
        _anchorSaver.SaveAnchorList(_anchorspotManager.anchorspots); //save to long term memory
        */
    }

    public void InitializeGameState()
    {
        //here we have the anchors we need active and the memoryslots we need active and in a list
        //we should hide all unused anchorspots
        //this should also be launched when the game is started and the scene is loaded correctly like last saved
        foreach (var anchorspot in _anchorspotManager.anchorspots)
        {
            if (anchorspot.IsChosen)
            {
                anchorspot.GameStateActive(true);
            }
            else
            {
                anchorspot.GameStateActive(false);
            }
        }
        
    }

    public void SetupGameState()
    {
        //hera all acnhors should be visible and pickable
        foreach (var anchorspot in _anchorspotManager.anchorspots)
        {
            anchorspot.GameStateActive(true);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
