using UnityEngine;
using static SingletonBase;

public class GameDataManager : SingletonBase<GameDataManager>
{
    void Awake()
    {
        base.Awake();
        Debug.Log("GameDataManager 싱글턴 초기화");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
