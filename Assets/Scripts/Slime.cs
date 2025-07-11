using UnityEngine;
using UnityEngine.InputSystem;

public class Slime : MonoBehaviour
{
    public float speed;
    public float changeDirectionInterval;
    public Transform targetObject;
    bool isLive;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    private bool isDragging = false;
    private Vector3 offset;
    Cotrols controls;

    private Vector2 dirVec;
    private float timer;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        controls = new Cotrols();
        ChangeDirection();
    }

    void OnClick(InputAction.CallbackContext ctx)
    {
        // 클릭 시 오브젝트를 클릭했는지 레이캐스트 확인
        Vector2 screenPos = controls.Game.position.ReadValue<Vector2>();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            offset = transform.position - (Vector3)worldPos;
            isDragging = true;
        }
    }

    void OnRelease(InputAction.CallbackContext ctx)
    {
        isDragging = false;
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            Vector2 screenPos = controls.Game.position.ReadValue<Vector2>();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z) + offset;
        }
        else
        {
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);

            timer += Time.fixedDeltaTime;
            if (timer >= changeDirectionInterval)
            {
                ChangeDirection();
                timer = 0f;
            }
        }
    }

    void ChangeDirection()
    {
        dirVec = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    private void LateUpdate()
    {
        spriter.flipX = dirVec.x < 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Inpipe"))
        {
            return;
        }
        PoolManager.Instance.Get(1);
        Dead();
    }

    private void OnEnable()
    {
        controls.Game.Enable();
        controls.Game.Click.performed += OnClick;
        controls.Game.Click.canceled += OnRelease;
        if (targetObject != null)
        {
            transform.position = targetObject.position;
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
