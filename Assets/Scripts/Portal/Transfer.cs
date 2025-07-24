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
            PoolManager.Instance.DeSpawnToPool(collider.gameObject);
            SpawnManager.Instance.SpawnSlime(transferTag, toPosition.position);
        }
        
    }
}
