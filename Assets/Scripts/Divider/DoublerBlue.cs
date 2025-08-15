using UnityEngine;

public class DoublerBlue : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        IDividable dividable = other.GetComponent<IDividable>();
        IAbsorbable absorbable = other.GetComponent<IAbsorbable>();
        Debug.Log(dividable.IsDivided);
        if (dividable != null && !dividable.IsDivided && absorbable != null)
        {
            GameObject spawnedObject = SpawnManager.Instance.SpawnSlime("Machine", other.transform.position, absorbable.DivideConcentration);
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

