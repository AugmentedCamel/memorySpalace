using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BubbleData : MonoBehaviour
{
    [SerializeField] private SpeechBubble _speechBubble;
    [SerializeField] private ObjectBubble _objectBubble;
    [SerializeField] private BubbleMenuController _bubbleMenuController;
    [SerializeField] private TextDisplayer _textDisplayer;
    
    [SerializeField] private int slotId;
    [SerializeField] private string objectString;
    [SerializeField] private string text;
    [SerializeField] private string audio;
    [SerializeField] private AudioClip audioClip;
    public int SlotId { get { return slotId; } }
    public string ObjectString { get { return objectString; } }
    public string Text { get { return text; } }
    public string Audio { get { return audio; } }

    /// <summary>
    /// Returns all the data the bubble has in type of BubbleSaveData.
    /// </summary>
    public BubbleSaveData GetBubbleData()
    {
        return new BubbleSaveData(_speechBubble.GetBubbleText(), _speechBubble.GetBubbleAudioClipAsString(), _objectBubble.GetCurrentObjectString(), 
            _speechBubble.GetAudioClipChannels(), _speechBubble.GetAudioClipFrequency(), _speechBubble.GetAudioClipSamples());
    }

    /// <summary>
    /// Sets all the data of the bubble from passed BubbleSavedData.
    /// </summary>
    public void SetBubbleData(BubbleSaveData bubbleData)
    {
        _speechBubble.SetBubbleText(bubbleData.text);
        _speechBubble.SetBubbleAudioClipFromString(bubbleData.audioClipString, bubbleData.channels, bubbleData.frequency, bubbleData.lengthSamples);
        Debug.Log(bubbleData.objectNames);
        _objectBubble.AssignObjectFromString(bubbleData.objectNames);

        text = bubbleData.text;
        objectString = bubbleData.objectNames;
        
        _textDisplayer.UpdateText();
        // gameObject.GetComponent<BubbleGameStateController>().OnActivation();
        // _bubbleMenuController.ToPage(2);
    }

    
    
    
    public bool IsEmpty
    {
        get { return slotId == 0 && string.IsNullOrEmpty(objectString) && string.IsNullOrEmpty(text) && string.IsNullOrEmpty(audio); }
    }
    
    public void EmptyData()
    {
        slotId = 0; // 0 is empty
        objectString = "";
        text = "";
        audio = "";
    }
    
    public void SetData(int slotId, string objectString, string text, string audio, AudioClip audioClip)
    {
        this.slotId = slotId;
        this.objectString = objectString;
        this.text = text;
        this.audio = audio;
        this.audioClip = audioClip;
    }   
    
    
    public void LoadEmptyBubble()
    {
        SetData(
            
            0,
            "",
            "",
            "",
            audioClip: null
        );
        _speechBubble.DeleteData();
    }

    public void LoadBubbleData(Guid uuidb)
    {
        //load the data from the database
        
        //set the data
        SetData(
            1,
            "to be added",
            "to be added",
            "to be added",
            audioClip: null
        );
    }
    
    //this method is to check for the last data set by the user
    [Button]
    public void UpdateBubbleData()
    {
        int newSlotId = slotId;
        if (slotId == 0) //this means that this is a new bubble
        {
            //should generate a new slot id
            System.Random random = new System.Random();
            newSlotId = random.Next(1, 1000);
        }
        
        string newObjectstring = "to be added";
        string newText = _speechBubble.GetBubbleText();
        string newAudio = "to be added"; //_speechBubble.GetBubbleAudioClipAsString();
        AudioClip newAudioClip = null;
        
        SetData(
            newSlotId,
            newObjectstring,
            newText,
            newAudio,
            newAudioClip
        );
    }
    
    private void OnBubbleLoad()
    {
        if (_speechBubble != null)
        {
            _speechBubble.DeleteData();
        }
        
        Debug.Log("Empty Bubble loaded");
        //should reset the text
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}