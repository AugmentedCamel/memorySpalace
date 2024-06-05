using System;
using System.Collections;
using System.Collections.Generic;
using Meta.XR.BuildingBlocks;
using NaughtyAttributes;
using TMPro;
using UnityEngine;


public class SpatialAnchorManager : MonoBehaviour
{
    public OVRSpatialAnchor anchorPrefab;
    [SerializeField] private GameObject _newAnchorParentPrefab;
    //[SerializeField] private SpatialAnchorSpawnerBuildingBlock _spatialAnchorSpawner;
    
    [SerializeField] private AnchorLoader _anchorLoader;
    public const string NumUuidsPlayerPref = "numUuids";
    
    private Canvas _canvas;
    private TextMeshProUGUI _uuidText;
    private TextMeshProUGUI _savedStatusText;
    
    [SerializeField] public List<OVRSpatialAnchor> _anchors = new List<OVRSpatialAnchor>();
    private OVRSpatialAnchor _lastCreatedAnchor;
    
    
    // Start is called before the first frame update
    public void CreateSpatialAnchor(Vector3 pos, Quaternion rot) //this is called when the user wants to save a memory slot
    {
        
        
        OVRSpatialAnchor workingAnchor = Instantiate(anchorPrefab, pos, rot);
        
        _canvas = workingAnchor.gameObject.GetComponentInChildren<Canvas>();
        _uuidText = _canvas.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _savedStatusText = _canvas.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        StartCoroutine(AnchorCreated(workingAnchor));
        
        //_spatialAnchorSpawner.SpawnSpatialAnchor(pos, rot);
        
        
    }
    
    
    private IEnumerator AnchorCreated(OVRSpatialAnchor workingAnchor)
    {
        while (!workingAnchor.Created && !workingAnchor.Localized)
        {
            yield return null;
        }
        Guid anchorId = workingAnchor.Uuid;
        _anchors.Add(workingAnchor);
        _lastCreatedAnchor = workingAnchor;
        
        _uuidText.text = "UUID: " + anchorId.ToString();
        _savedStatusText.text = "Not Saved";
        
        //automatically save after creation:
        SaveLastCreatedAnchor();
    }

    public async void SaveLastCreatedAnchorAsync()
    {
        var result = await _lastCreatedAnchor.SaveAsync();
        if (result)
        {
            _savedStatusText.text = "Saved";
            Debug.Log($"Anchor {_lastCreatedAnchor.Uuid} saved successfully.");
            
            SaveUuidToPlayerPrefs(_lastCreatedAnchor.Uuid);
        }
        else
        {
            _savedStatusText.text = "Failed to Save";
        }
    }
    
    [Button]
    public void SaveLastCreatedAnchor() //this is called from the memoryslot to save the anchor
    {
        
        SaveLastCreatedAnchorAsync();
    }

    void SaveUuidToPlayerPrefs(Guid uuid)
    {
        if (!PlayerPrefs.HasKey(NumUuidsPlayerPref))
        {
            PlayerPrefs.SetInt(NumUuidsPlayerPref, 0);
        }
        
        int playerNumUuids = PlayerPrefs.GetInt(NumUuidsPlayerPref);
        PlayerPrefs.SetString("uuid" + playerNumUuids, uuid.ToString());
        PlayerPrefs.SetInt(NumUuidsPlayerPref, ++playerNumUuids);
    }
    
    [Button]
    public async void UnSaveLastCreatedAnchor()
    {
        var result = await _lastCreatedAnchor.EraseAsync();
        if (result)
        {
            _savedStatusText.text = "not saved";
            Debug.Log($"Anchor {_lastCreatedAnchor.Uuid} unsaved successfully.");
        }
        else
        {
            _savedStatusText.text = "Failed to Unsave";
        }
        
    }

    [Button]
    public void UnsaveAllAnchors()
    {
        foreach (var anchor in _anchors)
        {
            EraseSavedAnchors(anchor);
            
        }
        
        _anchors.Clear();
        ClearAllUuidsFromPlayerPrefs();
    }

    async void EraseSavedAnchors(OVRSpatialAnchor anchor)
    {
        var results = await anchor.EraseAsync();
        if (results)
        {
            Debug.Log($"Anchor {anchor.Uuid} erased successfully.");
            var text = anchor.gameObject.GetComponentInChildren<Canvas>().gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = "Not Saved";
                Destroy(anchor.gameObject);
            }
            
            DestroyAnchor(anchor);
        }
        else
        {
            Debug.Log($"Failed to erase anchor {anchor.Uuid}");
        }
    }
    
    private void DestroyAnchor(OVRSpatialAnchor anchor)
    {
        Destroy(anchor.gameObject);
        Debug.Log("Destroyed anchor");
    }
    
    private void ClearAllUuidsFromPlayerPrefs()
    {
        if (PlayerPrefs.HasKey(NumUuidsPlayerPref))
        {
            int playerNumUuids = PlayerPrefs.GetInt(NumUuidsPlayerPref);
            for (int i = 0; i < playerNumUuids; i++)
            {
                PlayerPrefs.DeleteKey("uuid" + i);
            }
            PlayerPrefs.DeleteKey(NumUuidsPlayerPref);
            PlayerPrefs.Save();
        }
    }
    
    

    [Button]
    public void LoadSavedAnchors()
    {
        _anchorLoader.LoadAnchorsByUuid();
        Debug.Log("Tried loading anchros by uuid");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
