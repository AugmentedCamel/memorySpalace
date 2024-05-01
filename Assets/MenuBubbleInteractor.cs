using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBubbleInteractor : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _startmaterial;
    [SerializeField] private Material _hovermaterial;
    [SerializeField] private Material _pressedmaterial;
    [SerializeField] private BubbleEventLauncher _bubbleEventLauncher;
    private Coroutine _activeCoroutine = null;
    
    
    public float triggerTimer;
    // Start is called before the first frame update

    private void Start()
    {
        _startmaterial = _renderer.material;
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FingerTrigger>())
        {
            Debug.Log("finger entered");
            ButtonHover();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FingerTrigger>())
        {
            Debug.Log("finger exited");
            if (_activeCoroutine != null)
            {
                StopCoroutine(_activeCoroutine);
                _activeCoroutine = null;
                ButtonDeactivated();
            }
        }
    }
    
    private void ButtonHover() //hover
    {
        Debug.Log("Button Hover");
        _activeCoroutine = StartCoroutine(TriggerTimer());
        _renderer.material = _hovermaterial;
    }

    private void ButtonActivated() //pressed
    {
        Debug.Log("Button Activated");
        _renderer.material = _pressedmaterial;
        _activeCoroutine = StartCoroutine(DeactivateButton());
        
        _bubbleEventLauncher.LaunchBubbleEvent();
    }
    
    private void ButtonDeactivated() //unpressed
    {
        StopAllCoroutines();
        _activeCoroutine = null;
        _renderer.material = _startmaterial;
    }
    
    private IEnumerator TriggerTimer()
    {
        
        yield return new WaitForSeconds(triggerTimer);
        Debug.Log("Timer ended");
        ButtonActivated();
    }
    
    private IEnumerator DeactivateButton()
    {
        yield return new WaitForSeconds(3);
        ButtonDeactivated();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
