using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.Users;

public class DragManager : MonoBehaviour
{
    private ISelectable currentSelection;
    private IDraggable currentDragTarget;
    private GameObject currentDragObject;

    public Camera inCamera;

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }

        if (currentDragTarget != null && Input.GetMouseButton(0))
        {
            TargetToDrag(currentDragObject);
            currentDragTarget.Drag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            HandleRelease();
        }
    }

    private void HandleSelection()
    {
        Vector2 worldPoint = inCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null)
        {
            currentSelection?.Deselect();

            currentDragObject = hit.collider.gameObject;
            currentSelection = hit.collider.GetComponent<ISelectable>();
            currentDragTarget = hit.collider.GetComponent<IDraggable>();

            currentSelection?.Select();
            currentDragTarget?.BeginDrag();
        }
    }

    private void HandleRelease()
    {
        currentDragTarget?.EndDrag();
        currentDragTarget = null;
    }

    private void TargetToDrag(GameObject currObject)
    {
        Vector2 worldPos = inCamera.ScreenToWorldPoint(Input.mousePosition);

        currObject.transform.position = new Vector3(worldPos.x, worldPos.y, currObject.transform.position.z);
    }
}