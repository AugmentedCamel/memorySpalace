using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFiller : MonoBehaviour
{
    [SerializeField] private List<GameObject> _menuItemsToDisable;
    [SerializeField] private List<GameObject> _menuItemsToEnable;

    public bool isFillingData = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void FillData()
    {
        if (_menuItemsToDisable.Count > 0)
        {
            foreach (var menuItem in _menuItemsToDisable)
            {
                menuItem.SetActive(false);
            }
        }
        
        if (_menuItemsToEnable.Count > 0)
        {
            foreach (var menuItem in _menuItemsToEnable)
            {
                menuItem.SetActive(true);
            }
        }
        
        isFillingData = true;
    }
    
    public void StopFillingData()
    {
        if (_menuItemsToDisable.Count > 0)
        {
            foreach (var menuItem in _menuItemsToDisable)
            {
                menuItem.SetActive(true);
            }
        }
        
        if (_menuItemsToEnable.Count > 0)
        {
            foreach (var menuItem in _menuItemsToEnable)
            {
                menuItem.SetActive(false);
            }
        }
        
        isFillingData = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
