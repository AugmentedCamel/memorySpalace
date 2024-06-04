using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPlacement : MonoBehaviour
{
    public GameObject anchorPrefab;
    
    public void CreateSpatialAnchor(Vector3 position, Quaternion rotation)
    {
        var anchor = Instantiate(anchorPrefab, position, rotation);
        anchor.AddComponent<OVRSpatialAnchor>();
    }
    
}
