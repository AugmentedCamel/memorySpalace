using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerTrigger : MonoBehaviour
{
    public bool _isGrabbing = false;
    
    private DataHolder _dataHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void SetGrabbing(DataHolder dataHolder)
    {
        _isGrabbing = true;
        _dataHolder = dataHolder;
    }
    
    
    public void SetReleasing(GameObject obj)
    {
        _isGrabbing = false;
        if (_dataHolder != null)
        {
            _dataHolder.ReleaseObject(obj);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
