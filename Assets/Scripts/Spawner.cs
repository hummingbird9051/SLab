using UnityEngine;
using UnityEngine.InputSystem; // Input System 네임스페이스

public class Spawner : MonoBehaviour
{
    public PoolManager poolManager;
    Cotrols controls;
    void Awake()
    {
        controls = new Cotrols();
        controls.Game.Spawn.performed += ctx => Spawn();
    }

    void OnEnable()
    {
        controls.Game.Enable();
    }

    void OnDisable()
    {
        controls.Game.Disable();
    }

    void Spawn()
    {
        poolManager.Get(0);
    }
}
