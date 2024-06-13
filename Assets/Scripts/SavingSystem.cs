using System;
using System.Collections.Generic;
using System.Linq;
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

    /// <summary>
    /// Deletes given key from the PlayerPrefs.
    /// </summary>
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
        if (PlayerPrefs.HasKey(memorySlotType))
        {
            PlayerPrefs.DeleteKey(memorySlotType);
        }

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

            if (wrapper.bubblesData.Count > 1)
            {
                for(int i = 0; i < wrapper.bubblesData.Count - 1; i++)
                {
                    bubbleManager.AddBubble();
                }
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

    /// <summary>
    /// Saves bubbles for the current gameslot from anchors that have been saved to PlayerPref. (that are type of SavedAnchorPrefab)
    /// </summary>
    public void SaveAnchors()
    {
        List<BubbleManager> anchors = FindObjectsOfType<BubbleManager>().ToList();

        foreach (var anchor in anchors)
        {
            if (IsUuidSaved(anchor.GetUUID()))
            {
                anchor.SaveBubbles();
            }
        }
    }

    /// <summary>
    /// Loads bubbles of anchor based on the current gameslot.
    /// </summary>
    public void LoadAnchors()
    {
        List<BubbleManager> anchors = FindObjectsOfType<BubbleManager>().ToList();

        foreach (var anchor in anchors)
        {
            if (IsUuidSaved(anchor.GetUUID()))
            {
                anchor.LoadBubbles();
            }
        }
    }

    /// <summary>
    /// Deletes the bubbles/data of anchors for the current gameslot.
    /// </summary>
    public void DeleteAnchorsData()
    {
        List<BubbleManager> anchors = FindObjectsOfType<BubbleManager>().ToList();

        foreach (var anchor in anchors)
        {
            if (IsUuidSaved(anchor.GetUUID()))
            {
                anchor.DeleteBubbleData();
                anchor.DeleteBubbles();
            }
        }
    }

    /// <summary>
    /// Returns true if uuid of the anchor has been saved to PlayerPrefs, otherwise false.
    /// </summary>
    public bool IsUuidSaved(string uuid)
    {
        bool isSaved = false;
        
        int playerNumUuids = PlayerPrefs.GetInt(NumUuidsPlayerPref);

        for (int i = 0; i < playerNumUuids; i++)
        {
            var uuidKey = "uuid" + i;
            if (PlayerPrefs.GetString(uuidKey) == uuid)
            {
                isSaved = true;
            }
        }
        return isSaved;
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
