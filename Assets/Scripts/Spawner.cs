using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
//스페이스바 누르면 스폰하는 스포너 - 나중에 본 게임에서 필요 없어지면 지우면 됨. 현재는 테스트용
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PoolManager.Instance.SpawnFromPool("GroundSlime", transform.position);
        }
    }
}
