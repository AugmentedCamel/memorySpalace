using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public bool lookAtCamera = true;
    [SerializeField] private GameObject _camera;
    
    
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if (_camera)
        {
            if (lookAtCamera)
            {
                transform.LookAt(_camera.transform);
            }
        }
        
    }
}
