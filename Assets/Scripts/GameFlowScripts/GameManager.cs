using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //game has 3 states, s
    //setup is when the game is started and the scene is loaded correctly like last saved
    
    
    public enum GameState
    {
        gameInit = 0,
        gameNeutral = 1,
        createNewSequence = 2,
        TestSequence = 3,
        Debug = 4
        
    }

    public GameState gameState = GameState.Debug;
    public bool gameLaunched = false;
    
    [SerializeField] private GameBaseAnchorController _gameBaseAnchorController;
    
    private GameState _lastGameState = GameState.Debug;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnSceneLoaded()
    {
        gameLaunched = true;
        
        //first initilize the game
        GameLaunch();
        
    }
    
    private void GameLaunch()
    {
        //check if there is any saved data
        if (_gameBaseAnchorController.CheckForSavedData())
        {
            //if there is saved data, load the data
            StartGameWithLoadedData();
            Debug.Log("game launched with data");
        }
        else
        {
            //if there is no saved data, set gamestate to init
            ChangeGameState(GameState.gameInit);
            Debug.Log("game lauched empty");
        }
        //if there is saved data, load the data
        //if there is no saved data, set gamestate to init
        //and optionally launch a tutorial
        
    }
    
    [Button]
    private void StartGameWithLoadedData()
    {
        _gameBaseAnchorController.LoadSavedAnchors();
        ChangeGameState(GameState.gameNeutral);
        //set the gamestate to neutral
        //and start the game
    }
    
    [Button]
    private void StartGameInit()
    {
        ChangeGameState(GameState.gameInit);
        //set the gamestate to init
        //and start the game
    }
    
    public void ChangeGameState(GameState state)
    {
        _lastGameState = gameState;
        gameState = state;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (gameState != _lastGameState)
        {
            switch (gameState)
            {
                case GameState.gameInit:
                    OnGameStateInitTrigger();
                    break;
                case GameState.gameNeutral:
                    OnGameStateNeutralTrigger();
                    break;
                case GameState.createNewSequence:
                    OnGameStateCreateNewSequenceTrigger();
                    break;
                case GameState.TestSequence:
                    OnGameStateTestSequenceTrigger();
                    break;
            }
        }
        _lastGameState = gameState;
    }
    
    private void OnGameStateInitTrigger()
    {
        //init the game
        
        //delete existing memoryspots
        //load anchorspots on MRUK scene anchors
        _gameBaseAnchorController.LoadAnchorSpots(); //this removes all saved anchors and spawns new possible ones on scene anchors.
        Debug.Log("Game Init");
        
    }
    
    private void OnGameStateNeutralTrigger()
    {
        //start the game
    }
    
    private void OnGameStateCreateNewSequenceTrigger()
    {
        //create a new sequence
    }
    
    private void OnGameStateTestSequenceTrigger()
    {
        //test the sequence
    }
}
