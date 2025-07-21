using System.Collections;
using UnityEngine;

public class GroundSlime : Slime
{
    protected Rigidbody2D rb;
    protected Vector2 lastVelocity;


    private bool isChangingDirection = false;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        lastVelocity = rb.linearVelocity;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            if (rb.linearVelocity.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (rb.linearVelocity.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //ContactPoint2D contact = collision.contacts[0];
        //GroundSlime selfContact = collision.collider.GetComponent<GroundSlime>();
        //if (selfContact == null)
        //{
        //    Debug.Log(contact.collider.gameObject);
        //    Vector3 reflectedVelocity = Vector3.Reflect(lastVelocity, contact.normal);
        //    rb.linearVelocity = reflectedVelocity;
        //}
    }


    public void OnAnimationLoopEnd()
    {
        if (!isChangingDirection)
        {
            StartCoroutine(ChangeDirectionAfterDelay(0.5f));
        }
    }


    private Vector2 DirChange()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
    
    private IEnumerator ChangeDirectionAfterDelay(float delay)
    {
        isChangingDirection = true;
        Animator animator = GetComponent<Animator>();

        rb.linearVelocity = Vector2.zero;
        if (animator != null) animator.speed = 0;
        yield return new WaitForSeconds(delay);

        if (animator != null) animator.speed = 1;
        rb.linearVelocity = DirChange();

        isChangingDirection = false;
    }

}
