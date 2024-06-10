using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class BubbleManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bubbleFrames = new List<GameObject>();  // List to store bubbles
    [SerializeField] private float _bubbleSpacing = 1.0f;  // Spacing between bubbles
    [SerializeField] private Transform _bubbleParent;      // Parent transform to hold bubbles
    [SerializeField] private GameObject _bubblePrefab;     // Prefab to instantiate bubbles
    
    // Method to add a bubble
    [Button("Add Bubble")]
    public void AddBubble()
    {
        if (_bubblePrefab == null)
        {
            Debug.LogError("Bubble prefab is null!");
            return;
        }

        GameObject newBubble = Instantiate(_bubblePrefab, _bubbleParent);
        _bubbleFrames.Add(newBubble);
        Debug.Log($"Bubble added: {newBubble.name}");
        UpdateBubblePositions();
    }

    // Method to remove the last added bubble
    [Button("Remove Last Bubble")]
    public void RemoveLastBubble()
    {
        if (_bubbleFrames.Count > 1)
        {
            GameObject bubble = _bubbleFrames[_bubbleFrames.Count - 1];
            if (bubble != null)
            {
                string bubbleName = bubble.name; // Store the name before destroying
                _bubbleFrames.RemoveAt(_bubbleFrames.Count - 1);
                Debug.Log($"Removing bubble: {bubbleName}");

                if (Application.isPlaying)
                {
                    Destroy(bubble);
                    Debug.Log($"Bubble removed: {bubbleName}");
                }
                else
                {
                    DestroyImmediate(bubble);
                    Debug.Log($"Bubble removed immediately: {bubbleName}");
                }

                // Clean up the list before updating positions
                CleanUpNullReferences();
                UpdateBubblePositions();
            }
            else
            {
                Debug.LogWarning("Bubble to remove is already null!");
                CleanUpNullReferences();
            }
        }
        else
        {
            Debug.LogWarning("No bubbles to remove!");
        }
    }

    // Method to clean up null references from the list
    private void CleanUpNullReferences()
    {
        //Debug.Log("Cleaning up null references...");
        for (int i = _bubbleFrames.Count - 1; i >= 0; i--)
        {
            if (_bubbleFrames[i] == null)
            {
                Debug.Log($"Removing null reference at index {i}");
                _bubbleFrames.RemoveAt(i);
            }
        }
    }

    // Method to update positions of bubbles
    private void UpdateBubblePositions()
    {
        CleanUpNullReferences();
        for (int i = 0; i < _bubbleFrames.Count; i++)
        {
            if (_bubbleFrames[i] != null)
            {
                _bubbleFrames[i].transform.localPosition = new Vector3(i * _bubbleSpacing, 0, 0);
                Debug.Log($"Updated position of bubble {i}: {_bubbleFrames[i].name}");
            }
        }
    }

    // Called when the script is enabled
    private void OnEnable()
    {
        #if UNITY_EDITOR
        EditorApplication.update += EditorUpdate;
        #endif
        CleanUpNullReferences();
    }

    // Called when the script is disabled
    private void OnDisable()
    {
        #if UNITY_EDITOR
        EditorApplication.update -= EditorUpdate;
        #endif
        CleanUpNullReferences();
    }

    // Called when a child object is added or removed
    private void OnTransformChildrenChanged()
    {
        CleanUpNullReferences();
    }

    // Update method for the editor
    private void EditorUpdate()
    {
        if (!Application.isPlaying)
        {
            CleanUpNullReferences();
        }
    }

    // Method to handle when a bubble is destroyed manually
    private void Update()
    {
        if (Application.isPlaying)
        {
            CleanUpNullReferences();
        }
    }
}
