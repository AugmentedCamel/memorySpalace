using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject[] menuItems;
    private GameObject _activeMenu;

    private bool _menuOpen = false;
    public bool _trainMenuOpened = false;
    
    public void ActivateInitMenu()
    {
        _trainMenuOpened = false;
        SetMenuActive(0);
    }

    
    public void ActivateMenuNeutral()
    {
        _trainMenuOpened = false;
        SetMenuActive(1);
    }

    public void ActivateMenuTraining()
    {
        _trainMenuOpened = true;
        SetMenuActive(2);
    }

    public void ActivateMenuSetMemo()
    {
        _trainMenuOpened = false;
        SetMenuActive(3);
    }

    private void SetMenuActive(int index)
    {
        _activeMenu = menuItems[index];

           
        //disable all other menu's
        foreach (var item in menuItems)
        {
            if (menuItems[index] != item)
            {
                item.SetActive(false);
            }
        }
    }

    public void OpenMenu()
    {
        Debug.Log("menu opened");
        if (!_menuOpen)
        {
            _activeMenu.SetActive(true);
            _menuOpen = true;
            
        }

    }
    
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
