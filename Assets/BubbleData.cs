using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BubbleData : MonoBehaviour
{
    [SerializeField] private SpeechBubble _speechBubble;
    
    [SerializeField] private int slotId;
    [SerializeField] private string objectString;
    [SerializeField] private string text;
    [SerializeField] private string audio;
    [SerializeField] private AudioClip audioClip;
    public int SlotId { get { return slotId; } }
    public string ObjectString { get { return objectString; } }
    public string Text { get { return text; } }
    public string Audio { get { return audio; } }
    
    

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