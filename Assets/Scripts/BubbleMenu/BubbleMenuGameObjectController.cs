using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMenuGameObjectController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _menuVariants;
    [SerializeField] private GameManager _gameManager;
    
    private GameManager.GameState _currentState;
    
    private int _currentVariant = 0;
    
    public void OnActivation() //whenever the bubble becomes active 
    {
        SetMenuVariant(_gameManager.gameState);
    }
    
    public void OnDeActivation() //whenever the bubble becomes inactive
    {
        DeactiveBothMenus();
    }
    private void SetMenuRecordingActive()
    {
        if (_menuVariants != null && _menuVariants.Count > 0)
        {
            _menuVariants[0].SetActive(true);
            _menuVariants[1].SetActive(false);
        }
    }

    private void SetMenuTrainingActive()
    {
        if (_menuVariants != null && _menuVariants.Count > 0)
        {
            _menuVariants[0].SetActive(false);
            _menuVariants[1].SetActive(true);
        }
    }
    
    private void DeactiveBothMenus()
    {
        if (_menuVariants != null && _menuVariants.Count > 0)
        {
            _menuVariants[0].SetActive(false);
            _menuVariants[1].SetActive(false);
        }
    }
    
    private void SetMenuVariant(GameManager.GameState state) //this is called from game manager to set the menu variant
    {
        _currentState = state;
        switch (state)
        {
            case GameManager.GameState.gameInit:
                DeactiveBothMenus();
                break;
            case GameManager.GameState.gameNeutral:
                DeactiveBothMenus();
                break;
            case GameManager.GameState.createNewSequence:
                SetMenuRecordingActive();
                break;
            case GameManager.GameState.TestSequence:
                SetMenuTrainingActive();
                break;
            case GameManager.GameState.Debug:
                DeactiveBothMenus();
                break;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //subscribe to the unity event ongamestatechange event
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
