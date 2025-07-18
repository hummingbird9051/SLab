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
        IAbsorbable absorbable = collider.GetComponent<IAbsorbable>();
        if (absorbable != null)
        {
            PoolManager.Instance.DeSpawnToPool(collider.gameObject);
            PoolManager.Instance.SpawnFromPool(transferTag, toPosition.position);
        }
        
    }
}
