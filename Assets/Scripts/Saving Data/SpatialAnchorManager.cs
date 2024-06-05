using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class SpatialAnchorManager : MonoBehaviour
{
    public OVRSpatialAnchor anchorPrefab;
    public const string NumUuidsPlayerPref = "numUuids";
    
    private Canvas _canvas;
    private TextMeshProUGUI _uuidText;
    private TextMeshProUGUI _savedStatusText;
    
    [SerializeField] private List<OVRSpatialAnchor> _anchors = new List<OVRSpatialAnchor>();
    private OVRSpatialAnchor _lastCreatedAnchor;
    
    
    // Start is called before the first frame update
    public void CreateSpatialAnchor(Vector3 pos, Quaternion rot) //this is called when the user wants to save a memory slot
    {
        OVRSpatialAnchor workingAnchor = Instantiate(anchorPrefab, pos, rot);
        
        _canvas = workingAnchor.gameObject.GetComponentInChildren<Canvas>();
        _uuidText = _canvas.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _savedStatusText = _canvas.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        StartCoroutine(AnchorCreated(workingAnchor));
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
    }

    public void SaveLastCreatedAnchor()
    {
        _lastCreatedAnchor.Save((_lastCreatedAnchor, succes) =>
        {
            if (succes)
            {
                _savedStatusText.text = "Saved";
            }
            else
            {
                _savedStatusText.text = "Failed to Save";
            }
        });
        SaveUuidToPlayerPrefs(_lastCreatedAnchor.Uuid);
        
        
    }

    void SaveUuidToPlayerPrefs(Guid uuid)
    {
        if (PlayerPrefs.HasKey(NumUuidsPlayerPref))
        {
            PlayerPrefs.SetInt(NumUuidsPlayerPref, 0);
        }
        
        int playerNumUuids = PlayerPrefs.GetInt(NumUuidsPlayerPref);
        PlayerPrefs.SetString("uuid" + playerNumUuids, uuid.ToString());
        PlayerPrefs.SetInt(NumUuidsPlayerPref, ++playerNumUuids);
    }
    
    public void UnSaveLastCreatedAnchor()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
