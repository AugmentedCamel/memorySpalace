using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject[] menuItems;
    
    private bool _menuOpen = false;
    
    [Button]
    public void OpenMenu()
    {
        Debug.Log("menu opened");
        if (!_menuOpen)
        {
            foreach (var item in menuItems)
            {
                item.SetActive(true);
            }
            _menuOpen = true;
            
        }

    }
    
    [Button]
    private void CloseMenu()
    {
        Debug.Log("menu closed");
        if (_menuOpen)
        {
            foreach (var item in menuItems)
            {
                item.SetActive(false);
            }
            _menuOpen = false;
        }
    }
    
    public void ClosedMenuAfterDelay(float time)
    {
        StartCoroutine(CloseMenuAfterTime(time));
    }
    
    private IEnumerator CloseMenuAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        CloseMenu();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
