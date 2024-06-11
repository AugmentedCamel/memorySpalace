using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTrainMenuPicker : MonoBehaviour
{
    [SerializeField] private GameObject _trainMenu;
    [SerializeField] private GameObject _defaultMenu;
    [SerializeField] private MenuController _trainingHandMenu;
    bool convertToTrain = false;
    bool convertToDefault = false;
    
    public void PickTrain()
    {
        if (convertToTrain) return;
        _trainMenu.GetComponent<BubbleMenuController>().ToPage(0);
        _defaultMenu.GetComponent<BubbleMenuController>().ToPage(0);
        _trainMenu.SetActive(true);
        _defaultMenu.SetActive(false);
        convertToTrain = true;
    }
    public void PickDefault()
    {
        if (convertToDefault) return;
        _trainMenu.GetComponent<BubbleMenuController>().ToPage(0);
        _defaultMenu.GetComponent<BubbleMenuController>().ToPage(0);
        _trainMenu.SetActive(false);
        _defaultMenu.SetActive(true);
        convertToDefault = true;
    }

    private void Update()
    {
      
        
        
    }

    private void OnDisable()
    {
        convertToDefault = false;
        convertToTrain = false;
    }
}
