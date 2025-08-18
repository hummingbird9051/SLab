using UnityEngine;

public class Restarter : MonoBehaviour
{
    public SpawnManager spawnManage;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SpawnManager.Instance.Restart();
            KingSlimeSpawner.Instance.SetKingIndex(0);
        }
    }
}
