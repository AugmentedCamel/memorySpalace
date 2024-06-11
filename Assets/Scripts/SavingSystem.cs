using System;
using System.Collections.Generic;
using UnityEngine;

//Bubble Data Save Type
[Serializable]
public class BubbleSaveData
{
    public string text;
    public string audioClipString;
    public string objectNames;
    
    //for the correct audio
    public int channels;
    public int frequency;
    public int lengthSamples;

    public BubbleSaveData(string text, string audioClipString, string objectNames, int channels, int frequency, int lengthSamples)
    {
        this.text = text;
        this.audioClipString = audioClipString;
        this.objectNames = objectNames;

        this.channels = channels;
        this.frequency = frequency;
        this.lengthSamples = lengthSamples;
    }
}

//Bubble Data Wrapper for the json serialization
[Serializable]
public class BubbleSaveDataListWrapper
{
    public List<BubbleSaveData> bubblesData;

    public BubbleSaveDataListWrapper(List<BubbleSaveData> bubblesData)
    {
        this.bubblesData = bubblesData;
    }
}

public class SavingSystem : MonoBehaviour
{
    public static SavingSystem Instance;
    public const string NumUuidsPlayerPref = "numUuids";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void DeleteData(string memorySlotType)
    {
        PlayerPrefs.DeleteKey(memorySlotType);
    }

    /// <summary>
    /// Saves bubbles data in PlayerPref with id given in memorySlotType.
    /// </summary>
    public void SaveBubbles(List<BubbleData> bubblesData, string memorySlotType)
    {
        List<BubbleSaveData> saveDataList = new List<BubbleSaveData>();
        foreach (BubbleData bubble in bubblesData)
        {
            saveDataList.Add(bubble.GetBubbleData());
        }

        string json = JsonUtility.ToJson(new BubbleSaveDataListWrapper(saveDataList));
        PlayerPrefs.SetString(memorySlotType, json);
        PlayerPrefs.Save();
    }
    
    /// <summary>
    /// Loads bubbles from PlayerPref based on the id passed in memorySlotType.
    /// </summary>
    public void LoadBubbles(BubbleManager bubbleManager, string memorySlotType)
    {
        if (PlayerPrefs.HasKey(memorySlotType))
        {
            string json = PlayerPrefs.GetString(memorySlotType);
            BubbleSaveDataListWrapper wrapper = JsonUtility.FromJson<BubbleSaveDataListWrapper>(json);

            foreach (BubbleSaveData saveData in wrapper.bubblesData)
            {
                bubbleManager.AddBubble();
            }

            for(int i = 0; i < bubbleManager._bubbleFrames.Count; i++)
            {
                bubbleManager._bubbleFrames[i].GetComponent<BubbleData>().SetBubbleData(wrapper.bubblesData[i]);
            }
        }
        else
        {
            Debug.LogWarning("No data found in PlayerPrefs.");
        }
    }

    public void SaveUuidToPlayerPrefs(Guid uuid)
    {
        if (!PlayerPrefs.HasKey(NumUuidsPlayerPref))
        {
            PlayerPrefs.SetInt(NumUuidsPlayerPref, 0);
        }
        
        int playerNumUuids = PlayerPrefs.GetInt(NumUuidsPlayerPref);
        PlayerPrefs.SetString("uuid" + playerNumUuids, uuid.ToString());
        PlayerPrefs.SetInt(NumUuidsPlayerPref, ++playerNumUuids);
    }
    
    public void ClearAllUuidsFromPlayerPrefs()
    {
        if (PlayerPrefs.HasKey(NumUuidsPlayerPref))
        {
            int playerNumUuids = PlayerPrefs.GetInt(NumUuidsPlayerPref);
            for (int i = 0; i < playerNumUuids; i++)
            {
                PlayerPrefs.DeleteKey("uuid" + i);
            }
            PlayerPrefs.DeleteKey(NumUuidsPlayerPref);
            PlayerPrefs.Save();
        }
    }
}
