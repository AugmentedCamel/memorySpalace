using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleData : MonoBehaviour
{
    private int slotId;
    private string objectString;
    private string text;
    private string audio;
    
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
    
    public void SetData(int slotId, string objectString, string text, string audio)
    {
        this.slotId = slotId;
        this.objectString = objectString;
        this.text = text;
        this.audio = audio;
    }
    
    public void LoadEmptyBubble()
    {
        SetData(
            
            0,
            "",
            "",
            ""
        );
        OnBubbleLoad();

    }
    
    private void OnBubbleLoad()
    {
        // Debug.Log("Bubble loaded");
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