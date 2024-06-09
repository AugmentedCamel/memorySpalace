using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGameStateController : MonoBehaviour
{
    private GameManager.GameState _currentState;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _inActiveBubble;
    [SerializeField] private GameObject _activeBubble;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void OnActivation() //whenever the bubble becomes active 
    {
        UpdateToGameState();
        SetMenuVariant(_currentState);
        
        _inActiveBubble.SetActive(false);
        _activeBubble.SetActive(true);
    }
    
    public void OnDeActivation() //whenever the bubble becomes inactive
    {
        _inActiveBubble.SetActive(true);
        _activeBubble.SetActive(false);
    }
    
    public void UpdateToGameState()
    {
        _currentState = _gameManager.gameState;
    }
    
    private void SetMenuVariant(GameManager.GameState state) //this is called from game manager to set the menu variant
    {
        _currentState = state;
        switch (state)
        {
            case GameManager.GameState.gameInit:
                SetBubblesInactive();
                break;
            case GameManager.GameState.gameNeutral:
                SetBubblesInactive();
                break;
            case GameManager.GameState.createNewSequence:
                SetBubblesInactive();
                break;
            case GameManager.GameState.TestSequence:
                SetBubblesInactive();
                break;
            case GameManager.GameState.Debug:
                
                break;
        }
    }

    private void SetBubblesInactive() //when the game returns to neutral, all the bubbles should become inactive
    {
        _inActiveBubble.SetActive(true);
        _activeBubble.SetActive(false);
    }

    public void EmptyData()
    {
        //should delete the voice memo data
        //should reset the score
        //should delete the 3d object
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
