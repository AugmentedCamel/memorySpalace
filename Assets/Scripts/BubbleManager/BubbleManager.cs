using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bubbleFrames = new List<GameObject>();  // List to store bubbles
    [SerializeField] private float _bubbleSpacing = 1.0f;  // Spacing between bubbles
    [SerializeField] private Transform _bubbleParent;      // Parent transform to hold bubbles
    [SerializeField] private Vector3 _activeBubbleScale = new Vector3(1.2f, 1.2f, 1.2f);  // Scale for active bubbles

    // Method to add a bubble
    [Button("Add Bubble")]
    public void AddBubble(GameObject bubblePrefab)
    {
        if (bubblePrefab == null)
        {
            Debug.LogError("Bubble prefab is null!");
            return;
        }

        GameObject newBubble = Instantiate(bubblePrefab, _bubbleParent);
        _bubbleFrames.Add(newBubble);
        Debug.Log($"Bubble added: {newBubble.name}");
        UpdateBubblePositions();
    }
    
    // Method to remove the last added bubble
    [Button("Remove Last Bubble")]
    public void RemoveLastBubble()
    {
        if (_bubbleFrames.Count > 0)
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
        for (int i = _bubbleFrames.Count - 1; i >= 0; i--)
        {
            if (_bubbleFrames[i] == null)
            {
                Debug.Log($"Removing null reference at index {i}");
                _bubbleFrames.RemoveAt(i);
            }
        }
    }

    // Method called when the script is destroyed to clean up missing references
    private void OnDestroy()
    {
        CleanUpNullReferences();
    }

    // Method to handle when a bubble is destroyed manually
    private void Update()
    {
        CleanUpNullReferences();
    }

    // Method to activate a bubble
    public void ActivateBubble(GameObject bubble)
    {
        if (_bubbleFrames.Contains(bubble))
        {
            bubble.SetActive(true);
            bubble.transform.localScale = _activeBubbleScale;
            Debug.Log($"Bubble activated: {bubble.name}");
            // Add visual effects
        }
    }

    // Method to deactivate a bubble
    public void DeactivateBubble(GameObject bubble)
    {
        if (_bubbleFrames.Contains(bubble))
        {
            bubble.SetActive(false);
            bubble.transform.localScale = Vector3.one;  // Reset scale
            Debug.Log($"Bubble deactivated: {bubble.name}");
            // Add visual effects
        }
    }

    // Method to handle external activation event
    public void OnBubbleActivated(GameObject bubble)
    {
        ActivateBubble(bubble);
    }

    // Method to handle external deactivation event
    public void OnBubbleDeactivated(GameObject bubble)
    {
        DeactivateBubble(bubble);
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
                // add physics-based interaction or visual connections
            }
        }
    }

    /*// Method to handle underlay logic if needed
    private void UpdateUnderlay()
    {
        // logic for underlay management
    }*/
}
