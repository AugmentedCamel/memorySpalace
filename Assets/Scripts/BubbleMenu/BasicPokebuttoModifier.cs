using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BasicPokebuttoModifier : MonoBehaviour
{
    public UnityEvent OnButtonSelected;
    public UnityEvent OnButtonDeselected;
    
    [Header("Visual")]
    [SerializeField] private TextMeshPro InfoText;
    [SerializeField] string InfoTextString;
    [Header("Button Settings")]
    [SerializeField] private Texture ButtonLogo;
    [SerializeField] private string ButtonTextString;
    [Header("Button Object")]
    [SerializeField] private TextMeshPro ButtonText;
    [SerializeField] private RawImage ButtonLogoImage;
    // Start is called before the first frame update
    void Start()
    {
        ButtonLogoImage.texture = ButtonLogo;
        ButtonText.text = ButtonTextString;
    }


    

    public void RunButtonSelected()
    {
        OnButtonSelected.Invoke();
        InfoText.text = InfoTextString;
    }
    
    public void RunButtonDeselected()
    {
        Invoke("DeselectedAfterDelay", 0.1f);
        //OnButtonDeselected.Invoke();
    }

    private void DeselectedAfterDelay()
    {
        OnButtonDeselected.Invoke();
    }
    
}
