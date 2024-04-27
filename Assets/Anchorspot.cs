using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class Anchorspot : MonoBehaviour
{
    public bool IsActive;
    public bool InSight = false;
    public string AnchorName;
    public GameObject _player;
    private MRUKAnchor _parentAnchor;
    private bool _toggleVisibility = false;
    
    //[SerializeField] private GameObject _player; // The player object
    [SerializeField] public float visibilityDistance = 5.0f;

    [SerializeField] private GameObject AnchorSpotFrame;
    
    private void Start()
    {
        
        _parentAnchor = GetComponentInParent<MRUKAnchor>();
        if (_parentAnchor == null)
        {
            Debug.Log("Anchorspot must be a child of an MRUKAnchor");
        }
        else
        {
            AnchorName = _parentAnchor.gameObject.name;
            
        }
        
        SetFrameOffset();
    }
    
    public void SetPlayer(GameObject player)
    {
        _player = player;
    }
  
    private void SetFrameOffset() //the frame should have an offset from the achor spot depending on the size of anchor
    {
        float offset = 0.2f;

        AnchorSpotFrame.transform.localPosition = new Vector3(0, 0, offset);
    }

    public void ActivateVisibility()
    {
        if (_toggleVisibility == false)
        {
            AnchorSpotFrame.SetActive(true);
            InSight = true;
            _toggleVisibility = true;
        }

    }

    public void DeactivateVisibility()
    {
        if (_toggleVisibility == true)
        {
            AnchorSpotFrame.SetActive(false);
            InSight = false;
            _toggleVisibility = false;
        }
    }

    private void Update()
    {
        if (InSight)
        {
            transform.LookAt(_player.transform.position);
        }
        
    }
   
    
}
