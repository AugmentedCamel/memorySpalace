using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPanelController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textPanel; 
    private string displayText;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetText(string text)
    {
        displayText = text;
        textPanel.text = displayText;
    }
}
