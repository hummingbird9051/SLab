using UnityEngine;

public class Spitter : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        IAbsorbable absorbable = other.GetComponent<IAbsorbable>();
        absorbable.BeSpitOut(new Vector2(
            Mathf.Pow(transform.GetChild(0).position.x - other.gameObject.transform.position.x, 3f), 
            Mathf.Pow(transform.GetChild(0).position.y - other.gameObject.transform.position.y, 3f) 
            ));
    }
}
