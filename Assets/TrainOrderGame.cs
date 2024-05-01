using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainOrderGame : MonoBehaviour
{
    public bool isGameActive = false;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private TextMeshPro _stopBubbleText;
    [SerializeField] private TextMeshPro _streakBubbleText;
    [SerializeField] private int _currentIndex;
    [SerializeField] private MemoryBubbleManager _memoryBubbleManager;

    private string _saveMainText;
    private string _saveStopBubbleText;
    private string _saveStreakBubbleText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void StartGame()
    {
        SaveMenuText();
        isGameActive = true;
        _mainText.text = "Activate the bubbles in the correct order!";
        InitializeGame();
    }
    
    private void SaveMenuText()
    {
        _saveMainText = _mainText.text;
        _saveStopBubbleText = _stopBubbleText.text;
        _saveStreakBubbleText = _streakBubbleText.text;
    }
    
    private void RestartGame()
    {
        isGameActive = false;
        InitializeGame();
    }

    private void InitializeGame()
    {
        _stopBubbleText.text = "Stop Game";
        _streakBubbleText.text = "0";
        _currentIndex = 0;
    }
    
    
    public void CheckIfOrderGameIsCorrect(MemorySlot memorySlot) //this gets called after every interaction with memory slot
    {
        if (isGameActive)
        {
            if (_memoryBubbleManager.CheckOrderMemorySlots(memorySlot, _currentIndex))
            {
                _currentIndex++;
                _streakBubbleText.text = _currentIndex.ToString();
                if (_currentIndex == _memoryBubbleManager.memorySlots.Count)
                {
                    _mainText.text = "You win!";
                    RestartGame();
                }
            }
            else
            {
                _mainText.text = "You lose!";
                RestartGame();
            }
        }

    }
    
    public void StopGame()
    {
        if (isGameActive)
        {
            _mainText.text = "Game stopped!";
            ResetText();
            isGameActive = false;
        }
        
    }

    private void ResetText()
    {
        _mainText.text = _saveMainText;
        _stopBubbleText.text = _saveStopBubbleText;
        _streakBubbleText.text = _saveStreakBubbleText;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
