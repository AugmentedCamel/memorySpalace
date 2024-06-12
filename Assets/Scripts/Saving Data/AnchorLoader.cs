using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Meta.XR.BuildingBlocks;
using TMPro;
using UnityEngine;

public class AnchorLoader : MonoBehaviour
{
    
    private OVRSpatialAnchor _anchorPrefab;
    private SpatialAnchorManager _spatialAnchorManager;
    
    List<OVRSpatialAnchor.UnboundAnchor> _unboundAnchors = new();

    private Action<OVRSpatialAnchor.UnboundAnchor, bool> _onLoadAnchor;
    // Start is called before the first frame update
    void Start()
    {
        _spatialAnchorManager = GetComponent<SpatialAnchorManager>();
        _anchorPrefab = _spatialAnchorManager.anchorPrefab;
        _onLoadAnchor = OnLocalized;
        
    }

    public void LoadAnchorsByUuid()
    {
        if (!PlayerPrefs.HasKey(SavingSystem.NumUuidsPlayerPref))
        {
            PlayerPrefs.SetInt(SavingSystem.NumUuidsPlayerPref, 0);
            
        }
        
        var playerUuidCount = PlayerPrefs.GetInt(SavingSystem.NumUuidsPlayerPref);
        
        if (playerUuidCount == 0)
        {
            return;
        }
        
        Debug.Log("player uuid count: " + playerUuidCount);
        
        var uuids = new Guid[playerUuidCount];
        for (int i = 0; i < playerUuidCount; i++)
        {
            var uuidKey = "uuid" + i;
            var currentUuid = PlayerPrefs.GetString(uuidKey);
            uuids[i] = new Guid(currentUuid);
        }
        
        Load(new OVRSpatialAnchor.LoadOptions
        {   
            Timeout = 0,
            StorageLocation = OVRSpace.StorageLocation.Local,
            Uuids = uuids
        });
    }
    
    private void Load(OVRSpatialAnchor.LoadOptions options)
    {
        OVRSpatialAnchor.LoadUnboundAnchors(options, anchors =>
        {
            if (anchors == null)
            {
                Debug.Log("Failed to load anchors");
                return;
            }

            foreach (var anchor in anchors)
            {
                Debug.Log("Loaded anchor");
                if (anchor.Localized)
                {
                    _onLoadAnchor(anchor, true);
                }
                else if (!anchor.Localizing)
                {
                    ExecuteLocalizeTask(anchor);
                    
                }
            }
        });
    }
    
    private async void ExecuteLocalizeTask(OVRSpatialAnchor.UnboundAnchor unboundAnchor)
    {
        var result = await unboundAnchor.LocalizeAsync(0);
        if (result)
        {
            _onLoadAnchor(unboundAnchor, true);
        }
        else
        {
            _onLoadAnchor(unboundAnchor, false);
        }
        
    }
    
    private void OnLocalized(OVRSpatialAnchor.UnboundAnchor unboundAnchor, bool success)
    {
        if (!success)
        {
            Debug.Log("Failed to localize anchor");
            return;
        }

        var pose = unboundAnchor.Pose;
        var spatialAnchor = Instantiate(_anchorPrefab, pose.position, pose.rotation);
        unboundAnchor.BindTo(spatialAnchor); //creating a permanent link inbwteen the anchor and the object

        if (spatialAnchor.TryGetComponent<OVRSpatialAnchor>(out var anchor))
        {
            var uuidText = anchor.GetComponentInChildren<Canvas>().gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            var savedStatusText = anchor.GetComponentInChildren<Canvas>().gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            
            uuidText.text = anchor.Uuid.ToString();
            savedStatusText.text = "Loaded from Device";
            
            _spatialAnchorManager._anchors.Add(anchor);
        }
    }
    
    void Update()
    {
        
    }
}
