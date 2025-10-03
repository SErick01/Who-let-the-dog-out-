using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton. This does not instantiate automatically.
/// GetInstance() should just be a single line function that's "return this;".
/// e.g. public class MyClassName : NonInstantiatingSingleton<MyClassName> {}
/// </summary>
public abstract class NonInstantiatingSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    protected static T m_Instance;

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed. Returning null.");
                return null;
            }

            lock (m_Lock)
            {
                return m_Instance;
            }
        }
    }

    private void Awake()
    {
        //reset shutting down on awake.
        if (m_ShuttingDown)
        {
            m_ShuttingDown = false;
        }
        
        //get the instance. 
        lock (m_Lock)
        {
            m_Instance = GetInstance();
        }

        OnAwake();
    }

    protected virtual void OnAwake() { }
    protected abstract T GetInstance();

    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }


    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }
}
