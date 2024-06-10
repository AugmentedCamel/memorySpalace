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
        newBubble.GetComponent<BubbleGameStateController>().LoadBubbleEmpty();
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

    // Method to update positions of bubbles around the parent
    private void UpdateBubblePositions()
    {
        CleanUpNullReferences();

        float angleStep = 360.0f / _bubbleFrames.Count;
        float radius = _bubbleSpacing * _bubbleFrames.Count / (2 * Mathf.PI);

        for (int i = 0; i < _bubbleFrames.Count; i++)
        {
            if (_bubbleFrames[i] != null)
            {
                float angle = i * angleStep * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;
                _bubbleFrames[i].transform.localPosition = new Vector3(x, 0, z);
                Debug.Log($"Updated position of bubble {i}: {_bubbleFrames[i].name}");
            }
        }
    }

    // Draw gizmo to visualize the bubble circle
    private void OnDrawGizmos()
    {
        if (_bubbleParent == null) return;

        Gizmos.color = Color.green;
        float radius = _bubbleSpacing * _bubbleFrames.Count / (2 * Mathf.PI);
        int segments = 100;
        Vector3 prevPoint = _bubbleParent.position + new Vector3(Mathf.Cos(0) * radius, 0, Mathf.Sin(0) * radius);

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * Mathf.PI * 2 / segments;
            Vector3 newPoint = _bubbleParent.position + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            Gizmos.DrawLine(prevPoint, newPoint);
            prevPoint = newPoint;
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
            UpdateBubblePositions();
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
