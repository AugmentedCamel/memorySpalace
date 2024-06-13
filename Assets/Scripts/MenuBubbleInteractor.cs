using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuBubbleInteractor : MonoBehaviour
{
    public UnityEvent onTriggerEnteredEvent = new UnityEvent();
    public UnityEvent onTriggerEExitedEvent = new UnityEvent();
    
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _startmaterial;
    [SerializeField] private Material _hovermaterial;
    [SerializeField] private Material _pressedmaterial;
    [SerializeField] private BubbleEventLauncher _bubbleEventLauncher;
    
    // [SerializeField] private TextMeshPro 
    
    private Coroutine _activeCoroutine = null;
    private bool _isMaterialLocked = false;
    
    public float triggerTimer;
    // Start is called before the first frame update

    private void Start()
    {
        _startmaterial = _renderer.material;
        
    }
    
    public void SetMaterialManual(int materialIndex)
    {
        switch (materialIndex)
        {
            case 0:
                SetMaterial(_startmaterial);
                break;
            case 1:
                SetMaterial(_hovermaterial);
                break;
            case 2:
                SetMaterial(_pressedmaterial);
                break;
        }
    }
    
    public void UnlockMaterial()
    {
        SetMaterial(_startmaterial); //back to default
        _isMaterialLocked = false;
    }
    
    private void SetMaterial(Material material) //only used for manual setting
    {
        _renderer.material = material;
        _isMaterialLocked = true;
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
        //Debug.Log("Button Hover");
        if (_activeCoroutine == null)
        {
            _activeCoroutine = StartCoroutine(TriggerTimer());
            if (!_isMaterialLocked) _renderer.material = _hovermaterial;
        }
    }

    private void ButtonActivated() //pressed
    {
        //Debug.Log("Button Activated");
        if (!_isMaterialLocked) _renderer.material = _pressedmaterial;
        _activeCoroutine = StartCoroutine(DeactivateButton());
        
        _bubbleEventLauncher.LaunchBubbleEvent();
        if (!_isMaterialLocked) _renderer.material = _startmaterial;
        onTriggerEnteredEvent.Invoke();
    }
    
    private void ButtonDeactivated() //unpressed
    {
        StopAllCoroutines();
        _activeCoroutine = null;
        if (!_isMaterialLocked) _renderer.material = _startmaterial;
        onTriggerEExitedEvent.Invoke();
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
