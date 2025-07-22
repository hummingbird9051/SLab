using UnityEngine;

public class Transfer : MonoBehaviour
{
    public string transferTag;

    public Transform toPosition;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        IAbsorbable absorbable = collider.GetComponent<IAbsorbable>(); // IAbsorbable을 구현해놓은 객체만 움직이게. 
        if (absorbable != null)
        {
            //(슬라임 갯수가 많아지면 여기 코드 수정 필요)
            PoolManager.Instance.DeSpawnToPool(collider.gameObject);
            PoolManager.Instance.SpawnFromPool(transferTag, toPosition.position);
            //------------------------------------------
        }
        
    }
}
