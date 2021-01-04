using UnityEngine;
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Variables
    protected static T inst;

    public static T Inst
    {
        get
        {
            if (inst == null)
            {
#if UNITY_EDITOR
                if(!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    //Debug.LogWarning("Creating singleton of type (" + typeof(T) + ") when quiting game!");
                }
#endif
                T[] objects = FindObjectsOfType<T>();
                if (objects.Length == 0)
                {
                    //Instantiate a new game object with the singleton script on
                    GameObject go = new GameObject(typeof(T).ToString());
                    inst = go.AddComponent<T>();
                }
                else
                {
                    // Set the first found object of this type as the instance
                    inst = objects[0];

                    if(objects.Length > 1)
                    {
#if UNITY_EDITOR
                        Debug.LogError("Found " + objects.Length + " singletons of type: (" + typeof(T) + ")!");
#endif
                    }
                }
            }
            return inst;
        }
    }

    public static bool IsInstanceSet
    {
        get { return (inst != null); }
    }

    public static bool ExistsInScene
    {
        get { return (FindObjectOfType<T>() != null); }
    }

    public static void DestroyInst()
    {
        if (!IsInstanceSet)
        {
#if UNITY_EDITOR
            Debug.LogError("can't destroy " + typeof(T) + " it doesn't exist");
#endif
            return;
        }
        Destroy(inst.gameObject);
        inst = null;
    }

    #endregion
    #region public
    #endregion
    #region Mono

    void Awake()
    {
        if (Inst == null)
        {
            inst = this as T;
        }
        else if(inst != this)
        {
            DestroyImmediate(gameObject);
        }
    }
    #endregion
}