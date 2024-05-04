using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFollower : MonoBehaviour
{
    [SerializeField] private Transform _handtarget;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_handtarget != null)
        {
            this.transform.position = _handtarget.position;
        }

    }
}
