using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTowardsTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    //this script adds a force to the object to float towards the target object unless its closer than he threshold
    [SerializeField] private float _threshold = 0.05f;
    [SerializeField] private float _force = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            if (Vector3.Distance(transform.position, _target.position) > _threshold)
            {
                Vector3 direction = (_target.position - transform.position).normalized;
                GetComponent<Rigidbody>().AddForce(direction * _force);
            }
        }
    }
    
    
    
}
