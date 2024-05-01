using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorSpotDetector : MonoBehaviour
{
    //create a list of nearby bubbleanchors using colliders trigger enter and exit
    public List<BubbleAnchor> NearbyBubbleAnchors = new List<BubbleAnchor>();
    
    private void OnTriggerEnter(Collider other)
    {
        //check if the object is a bubble anchor
        BubbleAnchor bubbleAnchor = other.GetComponent<BubbleAnchor>();
        if (bubbleAnchor != null)
        {
            //add the bubble anchor to the list
            NearbyBubbleAnchors.Add(bubbleAnchor);
            bubbleAnchor.ActivateBubbleFrame();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //check if the object is a bubble anchor
        BubbleAnchor bubbleAnchor = other.GetComponent<BubbleAnchor>();
        if (bubbleAnchor != null)
        {
            //remove the bubble anchor from the list
            NearbyBubbleAnchors.Remove(bubbleAnchor);
            bubbleAnchor.DeactivateBubbleFrame();
        }
    }
    
    
}
