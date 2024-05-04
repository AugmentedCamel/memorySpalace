using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class Anchorspot : MonoBehaviour
{
    public bool IsActive;
    public bool InSight = false;
    public bool IsChosen = false;
    public bool Hidden = false;
    public string AnchorName;
    public GameObject _player;
    private MRUKAnchor _parentAnchor;
    private bool _toggleVisibility = false;
    
   
    [SerializeField] public float visibilityDistance = 5.0f;
    [SerializeField] private GameObject _anchorParentCenter;
    [SerializeField] private GameObject _anchorSpotFrame;
    
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

        _anchorSpotFrame.transform.localPosition = new Vector3(0, 0, offset);
    }

    public void ActivateVisibility()
    {
        if (_toggleVisibility == false)
        {
            _anchorSpotFrame.SetActive(true);
            InSight = true;
            _toggleVisibility = true;
        }

    }

    public void DeactivateVisibility()
    {
        if (_toggleVisibility == true)
        {
            _anchorSpotFrame.SetActive(false);
            InSight = false;
            _toggleVisibility = false;
        }
    }
    
    public void GameStateActive(bool state)
    {
        if (state)
        {
            Debug.Log("toggeld on");
            ActivateVisibility();
            IsActive = true;
            Hidden = false;
        }
        else
        {
            Debug.Log("toggeld off");
            DeactivateVisibility();
            IsActive = false;
            Hidden = true;
        }
        
    }
    
    public void ChooseAnchor()
    {
        IsChosen = true;
    }

    private void Update()
    {
        if (InSight)
        {
            transform.LookAt(_player.transform.position);
        }

        if (!IsActive)
        {
            //should be invisible and not active
            DeactivateVisibility();
            //gameObject.SetActive(false);
        }
        
    }
   
    
}
