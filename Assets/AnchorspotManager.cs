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
    
    private int _frameCounter = 0;
    
    //this spawns all anchorspots in the scene and aligns them to the camera
    //this is called when the scene is loaded and there are no anchorspots saved.
    public void PopulateAnchorspots()
    {
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
    
    
    
    private void OnPopulatedAnchorSpots()
    {
        if (anchorspots.Count == 0) //this means that the scene has no anchorspots saved
        {
            PopulateAnchorspots();
        }
        
        foreach (Anchorspot anchorspot in anchorspots)
        {
            anchorspot.SetPlayer(_player.gameObject);
        }
        
    }
    
    private void CheckDistance(Anchorspot anchorspot)
    {
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
    
    private void Update()
    {
        _frameCounter++;
        if (_frameCounter == 10)
        {
            foreach (Anchorspot anchorspot in anchorspots)
            {
                if (anchorspot.IsActive)
                {
                    CheckDistance(anchorspot);
                }
            }
            _frameCounter = 0;
        }
        
    }
}
