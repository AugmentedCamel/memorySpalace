using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AnchorSaver : MonoBehaviour
{
    //this script saves the total amount of anchorspots to check if nothing has changes since last time
    //and this script saves the chosen anchorspots to memory
    [SerializeField] private AnchorspotManager _anchorspotManager;
    [SerializeField] private MemorySlotManager _memorySlotManager;
    
    private bool _isAnchorSlotMatching = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool OnAllAnchorsLoaded()
    {
        //check current amount of anchor slots and compare them to last saved amount
        int lastAmount = PlayerPrefs.GetInt("AnchorAmount");
        
        if (_anchorspotManager.anchorspots.Count == lastAmount)
        {
            //if they are the same, check if the chosen anchorspots are the same
            //if they are the same, do nothing
            Debug.Log("Anchorspot amount is the same as last time");
            _isAnchorSlotMatching = true;
            return true;
        }
        //in this case the scene anchors are not matching with last time
        Debug.Log("Anchorspot amount is not the same as last time");
        _isAnchorSlotMatching = false;
        return false;
    }
    
    
    public void SaveAnchorList(List<Anchorspot> anchorspots)
    {
        PlayerPrefs.SetInt("AnchorAmount", anchorspots.Count);
        //save the current amount of anchorspots to memory
        
        //save the chosen anchorspots to memory
        for (int i = 0; i < anchorspots.Count; i++)
        {
            if (anchorspots[i].IsChosen)
            {
                PlayerPrefs.SetInt("AnchorSlot" + i, 1);
                Debug.Log("AnchorSlot" + i + " is saved as 1");
            }else
            {
                PlayerPrefs.SetInt("AnchorSlot" + i, 0);
                Debug.Log("AnchorSlot" + i + " is saved as 0");
            }
        }
    }

    [Button]
    public void ResetMemory()
    {
        PlayerPrefs.SetInt("AnchorAmount", 0);
        for (int i = 0; i < _anchorspotManager.anchorspots.Count; i++)
        {
            PlayerPrefs.SetInt("AnchorSlot" + i, 0);
        }
        Debug.Log("memory resetted, first load or you added / removed a meta scene anchor");
    }
    
    public void LoadMemorySlots()
    {
        if (!_isAnchorSlotMatching)
        {
            Debug.Log("memory slots could not be loaded, the anchorspots are not matching with last time");
            //user should choose new memoryslots
            return;
        }
        
        Debug.Log("memory slots loading");
        //go through the anchorspots and check if they are in memory
        for (int i = 0; i < _anchorspotManager.anchorspots.Count; i++)
        {
            if (PlayerPrefs.GetInt("MemorySlot" + i) == 1)
            {
                _anchorspotManager.anchorspots[i].IsChosen = true;
            }
        }
        //_memorySlotManager.InitializeGameState(); //start the game with the chosen anchorspots
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
