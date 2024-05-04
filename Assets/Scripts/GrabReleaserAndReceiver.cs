using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabReleaserAndReceiver : MonoBehaviour
{
    [SerializeField] private FingerTrigger _fingerTrigger;

    [SerializeField] private GameObject _dataObject;
    [SerializeField] private GameObject _receivingBubble;
    public bool isReceivingBubble = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void GrabTriggerRelease(GameObject obj)
    {
        Debug.Log("GrabOrRelease trigger");
        
        if (_fingerTrigger._isGrabbing == false && isReceivingBubble == false)
        {
            Debug.Log("trying to grab object");
            if (obj.TryGetComponent<DataHolder>(out DataHolder dataHolder))
            {
                dataHolder.GrabObject(_fingerTrigger.gameObject);
                _fingerTrigger.SetGrabbing(dataHolder);
            }
            
        }

    }

    public void ReceivingTheData()
    {
        if (_fingerTrigger._isGrabbing == true && isReceivingBubble == true && _receivingBubble != null)
        {
            Debug.Log("trying to release object");
            _fingerTrigger.SetReleasing(_receivingBubble);
            
        }
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
