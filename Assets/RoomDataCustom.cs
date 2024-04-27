using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class RoomDataCustom : MonoBehaviour
{
    [SerializeField] private MRUK _mruk;
    // Start is called before the first frame update
    public Vector3 roomCenter;

    private void OnRoomDataLoaded()
    {
        Debug.Log("Room data loaded");
        MRUKRoom room = _mruk.GetCurrentRoom();
        roomCenter = room.GetRoomBounds().center;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
