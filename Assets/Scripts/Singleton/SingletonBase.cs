using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected void Awake()
    {
        if(Instance != null && Instance != gameObject.GetComponent<T>())
        {
            Destroy(gameObject);
            return;
        }

        Instance = gameObject.GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }
}
