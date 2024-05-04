using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAnchor : MonoBehaviour
{
   public bool isActive = false;
   [SerializeField] private List<GameObject> _anchorChilds;
   
   private void Start()
   {
       if (_anchorChilds.Count > 0)
       {
           foreach (var child in _anchorChilds)
           {
               child.SetActive(false);
               
           }
       }
       
       isActive = false;
 
   }
   
   public void ActivateBubbleFrame()
   {
      Debug.Log("ActivateBubbleFrame");
       if (_anchorChilds.Count > 0)
       {
           foreach (var child in _anchorChilds)
           {
               child.SetActive(true);
           }
           
           isActive = true;
       }
   }
   
   public void DeactivateBubbleFrame()
   {
       if (_anchorChilds.Count > 0)
       {
           foreach (var child in _anchorChilds)
           {
               child.SetActive(false);
           }
           
           isActive = false;
       }
   }
   
}
