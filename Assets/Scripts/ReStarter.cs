using UnityEngine;

public class Restarter : MonoBehaviour
{
    public SpawnManager spawnManage;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SpawnManager.Instance.Restart();
            SpawnManager.Instance.SetCurrentSlimeConcentration(8);
            KingSlimeSpawner.Instance.SetKingIndex(0);
        }
    }
}
