using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMenuPage : MonoBehaviour
{

    [SerializeField] private List<Transform> _bubbleMenuSlots;
    [SerializeField] private List<GameObject> _menuItem;
    [SerializeField] private Transform _defaultParent;
    void Start()
    {
      
    }
    
    public void EnablePage()
    {
        int index = 0;
        foreach (var slot in _bubbleMenuSlots)
        { 
            
            _menuItem[index].SetActive(true);
            _menuItem[index].transform.SetParent(slot);
            _menuItem[index].transform.localPosition = Vector3.zero;
            index++;
        }
        
    }
    
    public void DisablePage()
    {
        foreach (var item in _menuItem)
        {
            item.transform.SetParent(_defaultParent);
            item.SetActive(false);
            
        }
    }
    
    
}
