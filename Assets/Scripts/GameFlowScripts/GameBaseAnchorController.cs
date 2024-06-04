using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using NaughtyAttributes;
using UnityEngine;

public class GameBaseAnchorController : MonoBehaviour
{
    [SerializeField] private AnchorPrefabSpawner _anchorPrefabSpawner;
    [SerializeField] private MemoryBubbleManager _memoryBubbleManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void LoadAnchorSpots()
    {
        _memoryBubbleManager.memorySlots.Clear();
        _anchorPrefabSpawner.SpawnPrefabs(true);
        
    }
    
    [Button]
    public void SaveAnchorSpots()
    {
        //get the memory slots and create a new spatial anchor prefab to create a bubble spot.
        foreach (var memorySlot in _memoryBubbleManager.memorySlots)
        {
            memorySlot.SaveAnchorSpot();
        }
        
        _anchorPrefabSpawner.ClearPrefabs();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
