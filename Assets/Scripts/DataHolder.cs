using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10;
    
    private bool _isGrabbing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void GrabObject(GameObject obj)
    {
        //parent to object and set position to 0
        transform.parent = obj.transform;
        transform.localPosition = Vector3.zero;
        _isGrabbing = true;
        Debug.Log("Grabbing object");
    }
    
    public void ReleaseObject(GameObject obj)
    {
        //unparent object
        transform.parent = obj.transform;
        transform.localPosition = Vector3.zero;
        transform.localScale = obj.transform.localScale * 0.3f;
        _isGrabbing = false;
        Debug.Log("Releasing object");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1 * _rotationSpeed);
        
        if (_isGrabbing)
        {
            transform.localPosition = Vector3.zero;
        }
    }
}
