using System;
using UnityEngine;
    /// <summary>
    ///     Singleton for inheritance. Adds an Instance property to this class which can be used from other classes.
    ///     Does NOT create itself if it doesn't exist.
    /// </summary>
    /// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _instance;
    private static bool _awakeCalled;

    public static T Inst
    {
        get
        {
           /*  if(!_awakeCalled) // We should do a cleanup at some point and have this call here to mark any wrong calls to Inst
            {
                Debug.LogError("Never get any singleton instances in Awake()-calls!");
            } */
            
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
            }
            
            if (_instance == null)
            {
                Debug.LogWarning($"An instance of {typeof(T)} is needed in the scene. Don't access managers in OnValidate!");
            }

            //if (_instance == null)
            //    throw new NullReferenceException($"An instance of {typeof(T)} is needed in the scene, but there is none."); // Kill exceptions with fire!
            return _instance;
        }
    }
    
    public static bool IsInitialized => _instance != null;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            Destroy(this);
        }

        InitializeInstance();

        _awakeCalled = true;
    }

    /// <summary>
    ///     Initialize this instance.
    /// </summary>
    protected virtual void InitializeInstance()
    {
    }
}
