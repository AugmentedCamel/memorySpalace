using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    [SerializeField] private  TextMeshProUGUI textComponent;    // Reference to the Text component
    [SerializeField] private TextMeshPro sourceText;
    [SerializeField] private bool manualUpdate = false;
 

    void Start()
    {


    }

    private void Update()
    {
       
    }

    private void OnEnable()
    {
        if (!manualUpdate)
        {
            UpdateText();      
        }
      
    }


    // Call this method whenever the text is updated
    public void UpdateText()
    {
        string newText = sourceText.text;
        textComponent.text = newText;
        // Canvas.ForceUpdateCanvases();
        // scrollRect.verticalNormalizedPosition = 0f; // Scroll to the bottom
    }
    
    public void UpdateText(string newText)
    {
        textComponent.text = newText;
        // Canvas.ForceUpdateCanvases();
        // scrollRect.verticalNormalizedPosition = 0f; // Scroll to the bottom
    }
    public void ClearText()
    {
        textComponent.text = "";
        //_previousText = "";
    }
}