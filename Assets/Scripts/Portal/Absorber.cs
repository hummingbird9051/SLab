using UnityEngine;

public class Absorber : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collider)
    {
        IAbsorbable absorbable = collider.GetComponent<IAbsorbable>(); //IAbsorbable 인터페이스를 구현해놓은 객체만 빨아들이게
        if (absorbable != null)
        {
            absorbable.BeAbsorbed(transform.parent.transform);
        }
    }
}
