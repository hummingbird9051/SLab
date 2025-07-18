using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Slime : MonoBehaviour
{
    public static event Action OnSlimeEnabled;
    public static event Action OnSlimeDisabled;


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
