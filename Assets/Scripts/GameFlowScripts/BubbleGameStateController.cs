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
        //subscibe to the game manager event
        _gameManager.gameStateChangeEvent.AddListener(UpdateToGameState);
    }

    public void LoadBubbleEmpty()
    {
        OnAddition();
        
        
    }
    public void OnActivation() //whenever the bubble becomes active 
    {
        /*Debug.Log("BUBBLECONTROLLER Bubble Activated");
        UpdateToGameState();
        SetBubbleVariant(_currentState);
        */
        if (_currentState == GameManager.GameState.gameNeutral)
        {
            //should not be able to activate bubbles
            return;
        }
        
        _inActiveBubble.SetActive(false);
        _activeBubble.SetActive(true);
        _activeBubble.GetComponent<BubbleMenuGameObjectController>().OnActivation();
        
        Debug.Log("BUBBLECONTROLLER Bubble Activated");
    }
    
    public void OnDeActivation() //whenever the bubble becomes inactive
    {
        _inActiveBubble.SetActive(true);
        _activeBubble.SetActive(false);
        _activeBubble.GetComponent<BubbleMenuGameObjectController>().OnDeActivation();
    }
    
    public void OnAddition() //whenever the bubble is added
    {
        _inActiveBubble.SetActive(true);
        _activeBubble.SetActive(false);
        //should empty the data 
        EmptyData();
    }
    public void UpdateToGameState() //this should be laumched everytime the game state changes
    {
        _currentState = _gameManager.gameState;
        SetBubbleVariant(_currentState);
    }
    
    private void SetBubbleVariant(GameManager.GameState state) //this is called from game manager to set the bubble variant
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
                //empty all data and set the bubbles inactive
                break;
            case GameManager.GameState.TestSequence:
                SetBubblesInactive();
                //load in the data from the slot and set the bubbles inactive.
                break;
            case GameManager.GameState.Debug:
                
                break;
        }
    }

    private void SetBubblesInactive() //when the game state chagnes, all the bubbles should become inactive
    {
        _inActiveBubble.SetActive(true);
        _activeBubble.SetActive(false);
    }
    
    
    public void EmptyData()
    {
        GetComponent<BubbleData>().LoadEmptyBubble();
        //should delete the voice memo data
        //should reset the score
        //should delete the 3d object
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
