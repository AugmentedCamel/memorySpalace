using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpatialAnchorManager : MonoBehaviour
{
    public OVRSpatialAnchor anchorPrefab;
    
    private Canvas _canvas;
    private TextMeshProUGUI _uuidText;
    private TextMeshProUGUI _savedStatusText;
    
    private List<OVRSpatialAnchor> _anchors = new List<OVRSpatialAnchor>();
    private OVRSpatialAnchor _lastCreatedAnchor;
    
    
    // Start is called before the first frame update
    public void CreateSpatialAnchor(Transform trafo)
    {
        OVRSpatialAnchor workingAnchor = Instantiate(anchorPrefab, trafo.position, trafo.rotation);
        
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
