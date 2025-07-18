using UnityEngine;

public class Absorber : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collider)
    {
        IAbsorbable absorbable = collider.GetComponent<IAbsorbable>();
        if (absorbable != null)
        {
            absorbable.BeAbsorbed(transform.parent.transform);
        }
    }
}
