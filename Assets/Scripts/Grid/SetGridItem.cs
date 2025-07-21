using UnityEngine;
using UnityEngine.UI;

public class SetGridItem : MonoBehaviour
{
    public GameObject gridManagerObj;

    void Start()
    {
        GridManager gridManager = gridManagerObj.GetComponent<GridManager>();
        if (gridManager == null) return;
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int index = i;
                GameObject child = transform.GetChild(index).gameObject;
                if (child != null)
                {
                    Button btn = child.AddComponent<Button>();
                    btn.onClick.AddListener(() => gridManager.SetCurrentTileId(index));
                }
            }
        }
    }
}
