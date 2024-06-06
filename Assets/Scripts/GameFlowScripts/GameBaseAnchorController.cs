using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class GameBaseAnchorController : MonoBehaviour
{
    [SerializeField] private AnchorPrefabSpawner _anchorPrefabSpawner;
    [SerializeField] private MemoryBubbleManager _memoryBubbleManager;
    [SerializeField] private SpatialAnchorManager _spatialAnchorManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public bool CheckForSavedData()
    {
        if (!PlayerPrefs.HasKey(SpatialAnchorManager.NumUuidsPlayerPref))
        {
            PlayerPrefs.SetInt(SpatialAnchorManager.NumUuidsPlayerPref, 0);
            
        }
        
        var playerUuidCount = PlayerPrefs.GetInt(SpatialAnchorManager.NumUuidsPlayerPref);
        
        if (playerUuidCount == 0)
        {
            return false;
        }

        return true;
    }
    
    public void LoadAnchorSpots() //this is called to populate scene anchors with anchors that could be saved
    {
        _memoryBubbleManager.memorySlots.Clear();
        _anchorPrefabSpawner.SpawnPrefabs(true);
        
        //for now, delete all the anchors if we want to create new ones
        DeleteSavedAnchors();
        
    }
    
    private void DeleteSavedAnchors() //this is called to delete saved anchors
    {
        _spatialAnchorManager.UnsaveAllAnchors();
        Debug.Log("Deleting saved anchors");
    }
    
    public void LoadSavedAnchors() //this is called to load saved anchors
    {
        _spatialAnchorManager.LoadSavedAnchors();
        Debug.Log("Loading saved anchors");
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
