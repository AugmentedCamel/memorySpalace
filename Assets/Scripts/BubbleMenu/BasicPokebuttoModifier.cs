using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BasicPokebuttoModifier : MonoBehaviour
{
    public UnityEvent OnButtonSelected;
    public UnityEvent OnButtonDeselected;
    
    
    [Header("Button Settings")]
    [SerializeField] private Sprite ButtonLogo;
    
    [Header("Button Object")]
    [SerializeField] private TextMeshPro ButtonText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunButtonSelected()
    {
        
    }
    
    public void RunButtonDeselected()
    {
        
    }
    
}
