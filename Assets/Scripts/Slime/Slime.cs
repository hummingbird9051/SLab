using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Slime : MonoBehaviour
{
    public static event Action OnSlimeEnabled; //이벤트 액션. 모든 슬라임이 enable되었을 때 Invoke하도록 설정
    public static event Action OnSlimeDisabled; //이벤트 액션. 모든 슬라임이 disable되었을 때 Invoke하도록 설정


    void Start()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        OnSlimeEnabled?.Invoke();
    }

    void OnDisable()
    {
        OnSlimeDisabled?.Invoke();
    }

    
}
