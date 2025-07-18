using System.Collections;
using UnityEngine;

public class GroundSlime : Slime, IDraggable, ISelectable, IAbsorbable
{
    protected Rigidbody2D rb;
    protected Vector2 lastVelocity;

    private bool isDragging = false;
    private bool isChangingDirection = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    void Awake()
    {
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        lastVelocity = rb.linearVelocity;
        rb.linearVelocity = isDragging ? Vector2.zero : lastVelocity;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            if (rb.linearVelocity.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (rb.linearVelocity.x == 0)
            {
                spriteRenderer.flipX = spriteRenderer.flipX;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
            
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        Debug.Log(contact.collider.gameObject);
        Vector3 reflectedVelocity = Vector3.Reflect(lastVelocity, contact.normal);
        rb.linearVelocity = reflectedVelocity;
             
    }

    public void Select() { }

    public void Deselect() { }

    public void BeginDrag()
    {
        isDragging = true;
    }

    public void Drag()
    {
        isDragging = true;
    }

    public void EndDrag()
    {
        isDragging = false;
    }

    public void BeAbsorbed(Transform target)
    {
        rb.linearVelocity = target.position - transform.position;
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
