using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbsorbableSlime : GroundSlime, IDraggable, ISelectable, IAbsorbable
//머신으로 빨려 들어갈 수 있는 객체들(일반/귀족 슬라임). 따라서 드래그와 선택이 가능한 구조로 만듦
//(선택은 아직 만들지 않음.)
{

    public int DivideConcentration { get; set; }

    private bool isDragging = false;

    [SerializeField]
    float spawnDuration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        isDragging = false;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate(); //부모의 업데이트 함수 호출
        rb.linearVelocity = isDragging ? Vector2.zero : lastVelocity; //드래그 중이면 속도 변화 없도록
    }

    //ISelectable 구현
    public void Select() // when press the right mouse(Selection)
    {
        DivideObject();
    }

    public void Deselect() { }


    //IDraggable 구현
    public void BeginDrag() // when pres the left mouse(Begin Dragging)
    {
        isDragging = true;
    }

    public void Drag() // when pres the left mouse(is Dragging)
    {
        isDragging = true;
    }

    public void EndDrag() // when pres the left mouse(End Draggging)
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

    protected override void OnEnable() 
    {
        base.OnEnable();

        StartCoroutine(SpawnCoroutine());

        isDragging = false;
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(spawnDuration);

        while (DivideConcentration > 1)
        {
            DivideObject();
            
            yield return new WaitForSeconds(spawnDuration);
        }
    }

    void DivideObject()
    {
        SpawnManager.Instance.SpawnSlime("Ground", transform.position, DivideConcentration / 2);
        SpawnManager.Instance.SpawnSlime("Ground", transform.position, DivideConcentration / 2);
        gameObject.SetActive(false);
    }
}
