using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Meta.XR.MRUtilityKit;
using UnityEngine;



public class AnchorspotManager : MonoBehaviour
{
    public List<Anchorspot> anchorspots = new List<Anchorspot>();
    private Anchorspot currentAnchorspot;
    [SerializeField] private MRUK _mruk;
    [SerializeField] private Transform _player;
    [SerializeField] private MemorySlotManager _memorySlotManager;
    [SerializeField] private AnchorSaver _anchorSaver;
    
    private int _frameCounter = 0;
    
    //this spawns all anchorspots in the scene and aligns them to the camera
    //this is called when the scene is loaded and there are no anchorspots saved.
    public void PopulateAnchorspots()
    {
        Debug.Log("Populating Anchorspots");
        //get all anchorspots in the scene
        Anchorspot[] anchorspotsInScene = FindObjectsOfType<Anchorspot>();
        foreach (Anchorspot anchorspot in anchorspotsInScene)
        {
            anchorspots.Add(anchorspot);
            Debug.Log("added anchorspot to list");
            //anchorspot.AlignForwardToCentre(roomCenter);
            
        }
        
        OnPopulatedAnchorSpots();
    }


    private bool CheckMemory() //un used
    {
        return _anchorSaver.OnAllAnchorsLoaded(); //true if the anchor amount is the same as last time
    }
    
    private void LoadMemory()
    {
        _anchorSaver.LoadMemorySlots();
    }
    
    private void ResetMemory()
    {
        //_anchorSaver.ResetMemory();
    }
    
    private void OnPopulatedAnchorSpots()
    {
        if (anchorspots.Count == 0) //this means that this is the first time the scene is loaded
        {
            PopulateAnchorspots();
        }
        /*
        if (CheckMemory())
        {
            //if the memory is the same as the current anchorspots, do nothing
            //Debug.Log("there are no scene anchors added since last time");
            //now we should also check the memory of the chosen anchorspots
            //LoadMemory();
        }
        else
        {
            Debug.Log("there are scene anchors added since last time");
            //if the memory is different The anchorspots are different from the last time the scene was loaded this means the player should choose new memoryslots
            //this means that the player should choose new memoryslots and that 
            //ResetMemory();
        }*/
        
        
        
        foreach (Anchorspot anchorspot in anchorspots)
        {
            anchorspot.SetPlayer(_player.gameObject);
        }
        
    }
    
    private void CheckDistance(Anchorspot anchorspot)
    {
        if (anchorspot.Hidden)
        {
            return;
        }
        float distance = Vector3.Distance(_player.position, anchorspot.transform.position);
        if (distance < 1)
        {
            anchorspot.ActivateVisibility();
            
        }
        else if (anchorspot.InSight)
        {
            anchorspot.DeactivateVisibility();
            
        }
    }

    private void CheckNearAnchorSpots()
    {
        Collider[] nearAnchorspotsColliders = Physics.OverlapSphere(_player.position, 10);
        Anchorspot[] nearAnchorspots = nearAnchorspotsColliders.Select(collider => collider.GetComponent<Anchorspot>()).Where(anchorspot => anchorspot != null).ToArray();
    
        foreach (Anchorspot anchorspot in nearAnchorspots)
        {
            Debug.Log("near anchorspot: " + anchorspot.name);
        }
    }
    
    public int GetCurrentChosenAmount()
    {
        int chosenAmount = 0;
        foreach (Anchorspot anchorspot in anchorspots)
        {
            if (anchorspot.IsChosen)
            {
                chosenAmount++;
            }
        }
        return chosenAmount;
        //Debug.Log("Chosen amount: " + chosenAmount);
    }
    private void Update()
    {
        CheckNearAnchorSpots();
        
        
        
    }
}
