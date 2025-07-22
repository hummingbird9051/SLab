using UnityEngine;

public class AbsorbableSlime : GroundSlime, IDraggable, ISelectable, IAbsorbable 
//머신으로 빨려 들어갈 수 있는 객체들(일반/귀족 슬라임). 따라서 드래그와 선택이 가능한 구조로 만듦
//(선택은 아직 만들지 않음.)
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
        base.FixedUpdate(); //부모의 업데이트 함수 호출
        rb.linearVelocity = isDragging ? Vector2.zero : lastVelocity; //드래그 중이면 속도 변화 없도록
    }

    //ISelectable 구현
    public void Select() { }

    public void Deselect() { }


    //IDraggable 구현
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


    //IAbsorbable 인터페이스 구현
    public void BeAbsorbed(Transform target)
    {
        rb.linearVelocity = target.position - transform.position;
    }

    public void BeSpitOut(Vector2 pow)
    {
        rb.linearVelocity = pow;
    }
}
