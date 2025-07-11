using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectMover : MonoBehaviour
{
    private SelectionManager selectionManager;
    private Vector3 offset;
    private bool isMoving = false;
    private Plane dragPlane;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectionManager = GetComponent<SelectionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit) && selectionManager.selectedObjects.Contains(hit.collider.gameObject))
            {
                isMoving = true;

                dragPlane = new Plane(Vector3.up, hit.point);
                offset = hit.transform.position - hit.point;
            }
        }

        if(isMoving && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if(dragPlane.Raycast(ray, out distance))
            {
                Vector3 targetPoint = ray.GetPoint(distance);
                foreach (var obj in selectionManager.selectedObjects)
                {
                    obj.transform.position = targetPoint + offset;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }
    }
}
