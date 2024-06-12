using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectBubble : MonoBehaviour
{
    #region SerializeField Variables - Need Reference
    
    [SerializeField] private Transform _objectPlaceHolder;
    [SerializeField] private AudioSource _objectAudio;
    
    #endregion

    #region Private Variables
    
    private Tuple<string, GameObject> _currentlyAssignedObject;
    private Coroutine _animation;
    private Vector3 _originalObjectSize;
    private Quaternion _originalObjectRotation;
    
    #endregion

    #region Private Methods for 3D Object

    private void OnEnable()
    {
        if (_animation == null && _currentlyAssignedObject != null)
        {
            _animation = StartCoroutine(AnimateScale(_currentlyAssignedObject.Item2));
        }
    }

    /// <summary>
    /// Sets the proper object transform properties when assigning object.
    /// Sets proper parent, rotation and size.
    /// </summary>
    private void SetObjectTransform(GameObject obj)
    {
        _originalObjectRotation = obj.transform.localRotation;
        obj.transform.position = _objectPlaceHolder.transform.position;
        obj.transform.parent = _objectPlaceHolder;
        _originalObjectSize = obj.transform.localScale;
        obj.transform.localRotation = _originalObjectRotation;
    }

    /// <summary>
    /// Start the assignment of the object and its animation.
    /// </summary>
    private void SetObject(GameObject obj)
    {
        SetObjectTransform(obj);
        PlayObjectEffects(obj);
    }

    /// <summary>
    /// Starts effects on object, visual and sound.
    /// </summary>
    private void PlayObjectEffects(GameObject obj)
    {
        if (_animation == null)
        {
            _animation = StartCoroutine(AnimateScale(obj));
        }

        _objectAudio.Play();
    }

    /// <summary>
    /// Return the count of 3D objects available in the manager.
    /// </summary>
    private int GetPrefabCount()
    {
        return ObjectsManager.Instance.GetObjectsCount();
    }
    
    /// <summary>
    /// Coroutine for object appear animation. Scales up the object.
    /// </summary>
    private IEnumerator AnimateScale(GameObject obj)
    {
        obj.transform.localScale *= 0.2f;
        
        yield return ScaleTo(obj, _originalObjectSize * 1.1f, 0.5f);
        yield return ScaleTo(obj, _originalObjectSize, 0.5f);

        _animation = null;
    }

    /// <summary>
    /// Coroutine to scale a specified object to a specified scale during given time.
    /// </summary>
    private IEnumerator ScaleTo(GameObject obj, Vector3 targetScale, float duration)
    {
        var currentScale = obj.transform.localScale;
        float time = 0;

        while (time < duration)
        {
            obj.transform.localScale = Vector3.Lerp(currentScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        obj.transform.localScale = targetScale;
    }
    
    #endregion

    #region Public Methods for 3D Object
    
    /// <summary>
    /// Called to first Assign an object to the bubble.
    /// </summary>
    public void AssignObject()
    {
        if (_currentlyAssignedObject != null)
        {
            DeleteObject();
        }
        
        int randomIndex = Random.Range(0, GetPrefabCount());
        GameObject obj = ObjectsManager.Instance.GetObjectAtIndex(randomIndex);

        _currentlyAssignedObject = new Tuple<string, GameObject>(obj.name, obj);
        ObjectsManager.Instance.RemoveObjectAtIndex(randomIndex);

        SetObject(obj);
        
        obj.SetActive(true);
    }

    /// <summary>
    /// Called to refresh the object upon users request.
    /// </summary>
    public void RefreshObject()
    {
        int randomIndex = Random.Range(0, GetPrefabCount());
        GameObject obj = ObjectsManager.Instance.GetObjectAtIndex(randomIndex);

        DeleteObject();
        
        _currentlyAssignedObject = new Tuple<string, GameObject>(obj.name, obj);
        ObjectsManager.Instance.RemoveObjectAtIndex(randomIndex);
        
        SetObject(obj);
        
        obj.SetActive(true);
    }

    /// <summary>
    /// Return currently assigned objects string name.
    /// </summary>
    /// <returns></returns>
    public string GetCurrentObjectString()
    {
        if (_currentlyAssignedObject != null)
        {
            return _currentlyAssignedObject.Item1;
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// Assigns object from a string name. Called to retrieve the object of the bubble.
    /// </summary>
    public void AssignObjectFromString(string name)
    {
        if (_currentlyAssignedObject != null)
        {
            DeleteObject();
        }
        
        GameObject obj = ObjectsManager.Instance.GetObjectByString(name);

        _currentlyAssignedObject = new Tuple<string, GameObject>(obj.name, obj);
        ObjectsManager.Instance.RemoveObject(obj);
        
        SetObject(obj);
        
        obj.SetActive(true);
    }

    /// <summary>
    /// Deletes object data from the bubble.
    /// </summary>
    public void DeleteObject()
    {
        StopAllCoroutines();
        _animation = null;

        if (_currentlyAssignedObject != null)
        {
            ObjectsManager.Instance.AddObject(_currentlyAssignedObject.Item2);
        }

        _currentlyAssignedObject.Item2.SetActive(false);
        _currentlyAssignedObject.Item2.transform.localScale = _originalObjectSize;
        _currentlyAssignedObject.Item2.transform.rotation = _originalObjectRotation;
        _currentlyAssignedObject.Item2.transform.parent = ObjectsManager.Instance.transform;
        _currentlyAssignedObject.Item2.transform.position = Vector3.zero;
        _currentlyAssignedObject = null;
    }
    
    #endregion
}
