using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFOV : MonoBehaviour
{
    //checks for trigger collisions if anchors are in sight of the player
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Anchorspot>())
        {
            other.gameObject.GetComponent<Anchorspot>().InSight = true;
            Debug.Log("Anchor in sight");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Anchorspot>())
        {
            other.gameObject.GetComponent<Anchorspot>().InSight = false;
        }
    }
}
