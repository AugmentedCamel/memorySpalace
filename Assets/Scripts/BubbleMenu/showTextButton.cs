using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showTextButton : MonoBehaviour
{
    [SerializeField] private GameObject _textPanel;
    [SerializeField] private BasicPokebuttoModifier _pokebuttoModifier;
    bool _isTextPanelActive = false;
    public void ShowText()
    {
        if (_isTextPanelActive)
        {
            _textPanel.SetActive(false);
            _isTextPanelActive = false;
            _pokebuttoModifier.SetInfoText("ShowText");
            _pokebuttoModifier.SetButtonText("ShowText");
        }
        else
        {
            _textPanel.SetActive(true);
            _isTextPanelActive = true;
            _pokebuttoModifier.SetInfoText("CloseText");
            _pokebuttoModifier.SetButtonText("CloseText");
        }
    }

    private void Start()
    {
        _textPanel.SetActive(false);
        _pokebuttoModifier.SetInfoText("ShowText");
        _pokebuttoModifier.SetButtonText("ShowText");
    }
}
