using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ControllerMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _player;
    [SerializeField] private MemorySlotManager _memoryslotManager;
    [SerializeField] private AnchorspotManager _anchorspotManager;
    [SerializeField] private TextMeshPro _memorySlotAmountText;

    [SerializeField] private List<GameObject> _menuStates;
    
    public UnityEvent OnReturnToMainMenu;
    public UnityEvent OnStartTrainOrder;
    
    //[SerializeField] private 
    private bool _menuToggled = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _menu.SetActive(false);
        
    }

    public void ToggleMenu()
    {
        if (!_menuToggled)
        {
            ActivateMenuHolder();
            _menuToggled = true;
            UpdateTextCurrentSlotAmount();
        }
    }
    
    public void DeActivateMenu()
    {
        if (_menuToggled)
        {
            CloseMenu();
        }
    }
    private void ActivateMenuHolder()
    {
        _menu.SetActive(true);
        Debug.Log("Menu Toggled");
    }
    
    
    public void CloseMenu()
    {
        _menu.SetActive(false);
        _menuToggled = false;
    }
    
    public void MainMenuLaunch()
    {
        OnReturnToMainMenu.Invoke();
        
    }

    public void StartTrainOrder()
    {
        OnStartTrainOrder.Invoke();
        Debug.Log("Train Order Invoked");
        
    }
    
    
    
    //if the button saved is pressed, I want to look in all the active anchorspots and save the ones that are activated to store
    public void SaveMemory()
    {
        Debug.Log("Memory Saved");
        //_memoryslotManager.OverrideMemory();
        
        _memoryslotManager.InitializeGameState();
        //get all active anchorspots
        //save them to memory
    }
    
    public void PickNewSlots()
    {
        Debug.Log("New Slots Picked");
        
        _memoryslotManager.SetupGameState();
        //get all active anchorspots
        //save them to memory
    }

    private void UpdateTextCurrentSlotAmount()
    {
        int memorySlots = 0;
        memorySlots = _anchorspotManager.GetCurrentChosenAmount();
        _memorySlotAmountText.text = memorySlots.ToString();
    }

    private void Update()
    {
        
    }
}
