using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class BubbleSaveData
{
    public int slotId;
    public string objectString;
    public string text;
    public string audio;
}

public class GameDataManager : MonoBehaviour
{
    [SerializeField] private int _nummberOfSavedAnchors;

    [SerializeField] private int _numberOfAnchors;
    [SerializeField] private char _gameSlot;
    [SerializeField] private int _numberOfBubbles;
    [SerializeField] private int _bubbleID;
    
    
    private string saveFileName = "saveData.es3";
    
    
    
    public void SaveData(int numberOfAnchors, char gameSlot, int numberOfBubbles, int bubbleID)
    {
        string data = $"{numberOfAnchors}-{gameSlot}-{numberOfBubbles}-{bubbleID}";

        ES3.Save<string>("saveData", data, saveFileName);

        Debug.Log("Data saved successfully!");
        
        
    }
    
    
    public void SaveData(int numberOfAnchors, char gameSlot, int numberOfBubbles, int bubbleID, List<BubbleSaveData> bubbleData)
    {
        // ... existing code ...
        string data = $"{numberOfAnchors}-{gameSlot}-{numberOfBubbles}-{bubbleID}";

        ES3.Save<List<BubbleSaveData>>("bubbleData", bubbleData, saveFileName);

        Debug.Log("Data saved successfully!");
    }

    public void LoadData()
    {
        if (ES3.FileExists(saveFileName) && ES3.KeyExists("saveData", saveFileName))
        {
            string data = ES3.Load<string>("saveData", saveFileName);
            Debug.Log($"Loaded Data: {data}");

            // Split the data into components
            string[] components = data.Split('-');
            if (components.Length == 4)
            {
                int numberOfAnchors = int.Parse(components[0]);
                char gameSlot = components[1][0];
                int numberOfBubbles = int.Parse(components[2]);
                int bubbleID = int.Parse(components[3]);

                Debug.Log($"Number of Anchors: {numberOfAnchors}");
                Debug.Log($"Game Slot: {gameSlot}");
                Debug.Log($"Number of Bubbles: {numberOfBubbles}");
                Debug.Log($"Bubble ID: {bubbleID}");
            }
            else
            {
                Debug.LogError("Loaded data is in an incorrect format.");
            }
        }
        else
        {
            Debug.LogError("No save data found.");
        }
    }

    // Example usage
    void Start()
    {
        // Save example data
        SaveData(0, 'A', 6, 2781);

        // Load example data
        LoadData();
    }
    
}
