using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpawnNearPosition : MonoBehaviour
{
    // spawn menu near the position 
    [SerializeField] private Transform _Headposition;
    [SerializeField] private float _distance = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnMenu();
    }
    
    [Button]
    public void SpawnMenu()
    {
        Debug.Log(_Headposition.position);
        Vector3 spawnPosition = _Headposition.position + _Headposition.forward * _distance;
        gameObject.transform.position = spawnPosition;
        gameObject.transform.rotation.eulerAngles.Set(0, _Headposition.rotation.eulerAngles.y, 0);
        gameObject.SetActive(true);
    }
}
