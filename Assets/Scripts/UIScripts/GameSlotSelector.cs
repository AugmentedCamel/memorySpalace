using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSlotSelector : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gameSlots;

    public int activeSlot = 0;
    private int _lastActiveSlot = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetActiveSlot(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnActivationSlotA()
    {
        SetActiveSlot(0);
    }
    
    public void OnActivationSlotB()
    {
        SetActiveSlot(1);
    }
    
    public void OnActivationSlotC()
    {
        SetActiveSlot(2);
    }
    
    public void SetActiveSlot(int slot)
    {
        GameSlotManager.Instance.SetCurrentSlot(slot);
        _lastActiveSlot = activeSlot;
        activeSlot = slot;
        SetMaterialOfActiveSlot();
        UnlockMaterialOfLastActiveSlot();
    }

    public void UnlockMaterialOfLastActiveSlot()
    {
        if (_lastActiveSlot >= 0 && _lastActiveSlot < _gameSlots.Count)
        {
            Transform childTransform = _gameSlots[_lastActiveSlot].transform.GetChild(1);
            MenuBubbleInteractor menuBubbleInteractor = childTransform.GetComponent<MenuBubbleInteractor>();
            if (menuBubbleInteractor != null)
            {
                menuBubbleInteractor.UnlockMaterial();
            }
        }
    }
    public void SetMaterialOfActiveSlot()
    {
        // Check if the active slot index is within the range of the _gameSlots list
        if (activeSlot >= 0 && activeSlot < _gameSlots.Count)
        {
            // Get the child GameObject of the active slot
            Transform childTransform = _gameSlots[activeSlot].transform.GetChild(1);

            // Try to get the MenuBubbleInteractor component from the child GameObject
            MenuBubbleInteractor menuBubbleInteractor = childTransform.GetComponent<MenuBubbleInteractor>();

            // If the MenuBubbleInteractor component exists
            if (menuBubbleInteractor != null)
            {
                // Call a method to set the material to int 2
                menuBubbleInteractor.SetMaterialManual(2);
            }
        }
    }
}
