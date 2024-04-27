using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerDistance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        GetComponent<Renderer>().material.SetFloat("_ViewerDistance", distance);
    }
}
