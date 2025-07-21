using UnityEngine;

public class AbsorbableSlime : GroundSlime, IDraggable, ISelectable, IAbsorbable
{


    private bool isDragging = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        rb.linearVelocity = isDragging ? Vector2.zero : lastVelocity;
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

    public void BeSpitOut(Vector2 pow)
    {
        rb.linearVelocity = pow;
    }
}
