using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
            PoolManager.Instance.SpawnFromPool("GroundSlime", transform.position);
        }
    }
}
