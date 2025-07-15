using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Slime : MonoBehaviour
{
    public static event Action OnSlimeEnabled;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        OnSlimeEnabled?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
