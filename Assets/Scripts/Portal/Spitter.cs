using UnityEngine;

public class Spitter : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other) //spitter trigger 내부에 있으면 바깥으로 밀어내도록 설정
    {
        IAbsorbable absorbable = other.GetComponent<IAbsorbable>(); //IAbsorbable로 객체를 저장해 인터페이스가 있는 객체만 사용하게.
        absorbable.BeSpitOut(new Vector2(
            Mathf.Pow(transform.GetChild(0).position.x - other.gameObject.transform.position.x, 3f), 
            Mathf.Pow(transform.GetChild(0).position.y - other.gameObject.transform.position.y, 3f) 
            ));
    }
}
