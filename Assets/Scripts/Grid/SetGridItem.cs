using System.Collections.Generic;
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
            if (KingSlimeSpawner.Instance.GetKingIndex() >= 1)
            {
                for (int i = 1; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
        

        GridShowingManager.Instance.SelectIndexAction += UpdateGrid;
    }

    private void UpdateGrid(int index)
    {
        Debug.Log($"{index} , {index + 1} Activate");
        transform.GetChild(index).gameObject.SetActive(true);
        transform.GetChild(index + 1).gameObject.SetActive(true);
    }
}
