using System.Collections;
using UnityEngine;

public class GroundSlime : Slime //그라운드에 있는 슬라임 일반/귀족/킹 슬라임 모두 해당
{
    protected Rigidbody2D rb; //Rigidbody 컴포넌트 넣을 객체
    protected Vector2 lastVelocity; //이전 마지막 속도


    private bool isChangingDirection = false; //현재 방향을 바꾸는 중인가?에 대한 부울 변수


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Slime에 RequireComponent로 있어야하는 Rigidbody 컴포넌트 가져오기
        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)); //시작할 때 각 객체 랜덤 속도 설정
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        lastVelocity = rb.linearVelocity;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); //슬라임 객체의 스프라이트 렌더러를 가져옴.
        if (spriteRenderer != null)
        {
            if (rb.linearVelocity.x < 0) //x속도가 음수일 때
            {
                spriteRenderer.flipX = true; //뒤집기
            }
            else if (rb.linearVelocity.x > 0) //x속도가 양수일 때
            {
                spriteRenderer.flipX = false; //그대로 가져가기
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        GroundSlime selfContact = collision.collider.GetComponent<GroundSlime>();
        if (selfContact == null) //컴포넌트로 GroundSlime객체 가져와 슬라임끼리는 부딪혀도 안튀어나가게 설정
        {
            Debug.Log(contact.collider.gameObject);
            Vector3 reflectedVelocity = Vector3.Reflect(lastVelocity, contact.normal);
            rb.linearVelocity = reflectedVelocity;
        }
    }


    public void OnAnimationLoopEnd() //한번 뛰고 방향을 바꾸기 위한 애니메이션 함수, 슬라임의 애니메이션 이벤트에 들어감
    {
        if (!isChangingDirection)
        {
            StartCoroutine(ChangeDirectionAfterDelay(0.5f));
        }
    }


    private Vector2 DirChange() // 속도 바꾸기
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
    
    private IEnumerator ChangeDirectionAfterDelay(float delay) //한번 뛰고 딜레이를 일부러 주면서 방향을 바꾸기 위한.
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
