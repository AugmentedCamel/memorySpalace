using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    #region Public Variables

    public static ObjectsManager Instance;

    #endregion

    #region SerializeField Variables - Needs References
    
    [SerializeField] private List<GameObject> _objects;
    
    #endregion
    
    #region Private Basic Methods
    
    /// <summary>
    /// Ensures Singleton Pattern - only one instance of the manager - easy access
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    #endregion

    #region Public Methods for Objects

    /// <summary>
    /// Returns the current count of objects that bubble can assign.
    /// </summary>
    public int GetObjectsCount()
    {
        return _objects.Count;
    }

    /// <summary>
    /// Returns an object based on string name, if it exists, otherwise returns null.
    /// </summary>
    public GameObject GetObjectByString(string name)
    {
        foreach (var obj in _objects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }

        return null;
    }
    
    /// <summary>
    /// Returns object on the given index, if doesnt exist, returns null.
    /// </summary>
    public GameObject GetObjectAtIndex(int index)
    {
        if (index >= GetObjectsCount()) return null;
        
        return _objects[index];
    }

    /// <summary>
    /// Removes the object at the given index.
    /// </summary>
    public void RemoveObjectAtIndex(int index)
    {
        _objects.Remove(_objects[index]);
    }
    
    /// <summary>
    /// Removes the given object from the list of objects.
    /// </summary>
    public void RemoveObject(GameObject obj)
    {
        _objects.Remove(obj);
    }

    /// <summary>
    /// Adds given object to the list of objects.
    /// </summary>
    public void AddObject(GameObject obj)
    {
        _objects.Add(obj);
    }

    #endregion
}
