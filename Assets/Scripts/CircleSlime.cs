using UnityEngine;

public class CircleSlime : MonoBehaviour
{
    public Transform targetObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MIpipe"))
        {
            PoolManager.Instance.Get(0);
            Dead();
        }
        else if (collision.CompareTag("doubler"))
        {
            Vector3 spawnPosition = collision.transform.position;
            spawnPosition.y -= 3f; // Decrease the Y-coordinate by 3 units
            
            PoolManager.Instance.Get(1).transform.position = spawnPosition;
            PoolManager.Instance.Get(1).transform.position = spawnPosition;
            Dead();
        }
        return;

    }


    private void OnEnable()
    {
        if (targetObject != null)
        {
            transform.position = targetObject.position;
        }
    }


    void Dead()
    {
        gameObject.SetActive(false);
    }
}
