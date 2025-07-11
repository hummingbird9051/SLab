using UnityEngine;
using static SingletonBase;

public class SceneManager : SingletonBase<SceneManager>
{
    void Awake()
    {
        base.Awake();
        Debug.Log("SceneManager 싱글턴 초기화");
    }
    
}
