//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.Users;

public class DragManager : MonoBehaviour
{
    private ISelectable currentSelection;
    private IDraggable currentDragTarget;
    private GameObject currentDragObject;

    public Camera inCamera; //해당 카메라 안에서만 선택이 가능하도록

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
        if(Input.GetMouseButtonDown(1))
        {
            HandleRightSelection();
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

    private void HandleRightSelection()
    {
        Vector2 worldPoint = inCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null)
        {
            currentSelection?.Deselect();
            currentSelection = hit.collider.GetComponent<ISelectable>();
            currentSelection?.Select();
        }
    }

    private void HandleSelection() 
     //마우스의 스크린 좌표를 월드 좌표로 바꿔서 IDraggable 컴포넌트가 존재하는 객체만 움직일 수 있게
    {
        Vector2 worldPoint = inCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null)
        {

            currentDragObject = hit.collider.gameObject;
            currentDragTarget = hit.collider.GetComponent<IDraggable>();
            currentDragTarget?.BeginDrag();
        }
    }

    private void HandleRelease() //마우스를 놓았을 때 작업
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