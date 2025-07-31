using UnityEngine;

public class TempSetter : MonoBehaviour
{
    public SpawnManager spawnManage;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PoolManager.Instance.RefreshPool();
            spawnManage.SetCurrentSlimeLevel(0);
        }
    }
}
