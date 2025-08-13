using UnityEngine;

public class Elasticity : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        Vector3 direction = collision.transform.position - transform.position - new Vector3(0.0f, -3.0f, 0.0f);

        rb.AddForce(direction * 100);
    }
}
