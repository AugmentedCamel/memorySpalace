using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class BubbleMenuController : MonoBehaviour
{
    public GameObject TextPanel;
    [SerializeField] [CanBeNull] private List<BubbleMenuPage>  menuPages;
    
    private int _currentPage = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (menuPages != null && menuPages.Count > 0)
        {
            menuPages[_currentPage].EnablePage();
        }
    }
    
    public void NextPage()
    {
        if (menuPages != null && menuPages.Count > _currentPage + 1)
        {
            menuPages[_currentPage].DisablePage();
            _currentPage++;
            menuPages[_currentPage].EnablePage();
        }
    }
    
    public void ToPage(int page)
    {
        if (menuPages != null && menuPages.Count > page)
        {
            menuPages[_currentPage].DisablePage();
            _currentPage = page;
            menuPages[_currentPage].EnablePage();
        }
    }
  
    
    
}
