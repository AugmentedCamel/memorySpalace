using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //game has 3 states, setup, fill data, train data, train order
    //setup is when the game is started and the scene is loaded correctly like last saved
    
    
    public enum GameState
    {
        setup = 0,
        fillData = 1,
        trainOrder = 2,
        trainData = 3
    }
    
    public GameState gameState = GameState.setup;
    [SerializeField] private MemorySlotManager _memorySlotManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void ActivateTrainOrderState()
    {
        gameState = GameState.trainOrder;
        SetMemorySlotsToTrainOrder();
        
    }

    private void SetMemorySlotsToTrainOrder()
    {
        foreach (var memoryslot in _memorySlotManager.memorySlots)
        {
            memoryslot.GetComponent<MemorySlotUIEvents>().ActivateButtonOrderTest(true);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
