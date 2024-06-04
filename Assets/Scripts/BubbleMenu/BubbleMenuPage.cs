using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMenuPage : MonoBehaviour
{

    [SerializeField] private List<Transform> _bubbleMenuSlots;
    [SerializeField] private List<GameObject> _menuItem;
    
    
    public void EnablePage()
    {
        foreach (var slot in _bubbleMenuSlots)
        {
            // disable all child objects
            foreach (Transform child in slot)
            {
                child.gameObject.SetActive(false);
            }
        }
        foreach (var item in _menuItem)
        {
            item.SetActive(true);
        }
    }
    
    
}
