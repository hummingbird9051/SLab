using UnityEngine;
using UnityEngine.UI;

public class SetGridItem : MonoBehaviour
{
    public GameObject gridManagerObj;

    public Sprite pressedSprite;

    void Start()
    {
        GridManager gridManager = gridManagerObj.GetComponent<GridManager>(); //게임 매니저 오브젝트에서 GridManager 컴포넌트 추출
        if (gridManager == null) return;
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++) // 각 자식의 순서를
            {
                if (i == 0) continue; // tile 0번은 빈 오브젝트로 GridManager에 명시해 두었으므로 비우기.
                int index = i;
                GameObject child = transform.GetChild(index).gameObject;
                if (child != null)
                {
                    Button btn = child.AddComponent<Button>();
                    btn.onClick.AddListener(() => gridManager.SetCurrentTileId(index)); //타일 순서로 정해 타일 배치

                    btn.transition = Selectable.Transition.SpriteSwap;
                    SpriteState spriteState = btn.spriteState;
                    spriteState.pressedSprite = pressedSprite;
                    btn.spriteState = spriteState;
                }
            }
        }
    }

    void Update()
    {
        if (SpawnManager.Instance.GetCurrentSlimeLevel() >= 1)
        {
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
        }
    }
}
