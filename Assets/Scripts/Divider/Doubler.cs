using UnityEngine;

public class Doubler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        IDividable dividable = other.GetComponent<IDividable>();
        Debug.Log(dividable.IsDivided);
        if (dividable != null && !dividable.IsDivided)
        {
            GameObject spawnedObject = SpawnManager.Instance.SpawnSlime("Machine", other.transform.position);
            spawnedObject.GetComponent<IDividable>().BeDivide();
            dividable.BeDivide();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        IDividable dividable = other.GetComponent<IDividable>();
        dividable.BeUndivide();
    }
}
