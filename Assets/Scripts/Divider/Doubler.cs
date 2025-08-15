using UnityEngine;

public class Doubler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        IDividable dividable = other.GetComponent<IDividable>();
        IAbsorbable absorbable = other.GetComponent<IAbsorbable>();
        Debug.Log(dividable.IsDivided);
        if (dividable != null && !dividable.IsDivided && absorbable != null && absorbable.DivideConcentration > 1)
        {
            GameObject spawnedObject = SpawnManager.Instance.SpawnSlime("Machine", other.transform.position, absorbable.DivideConcentration / 2);
            if (absorbable.DivideConcentration % 2 == 0)
            {
                absorbable.DivideConcentration /= 2;
            }
            else
            {
                absorbable.DivideConcentration = absorbable.DivideConcentration / 2 + 1;
            }
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
