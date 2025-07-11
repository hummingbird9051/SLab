using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionManager : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    private bool isDragging = false;

    public List<GameObject> selectedObjects = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition; 
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            SelectObjects();
        }
    }

    void OnGUI()
    {
        if(isDragging)
        {
            endPos = Input.mousePosition;
            Rect selectionRect = new Rect(startPos.x, Screen.height - startPos.y, endPos.x - startPos.x,
                (Screen.height - endPos.y) - (Screen.height - startPos.y));

            GUI.Box(selectionRect, "");
        }
    }

    void SelectObjects()
    {
        foreach (var obj in selectedObjects)
        {
            obj.GetComponent<Renderer>().material.color = Color.white;
        }
        selectedObjects.Clear();
        Rect selectionRect = new Rect(Mathf.Min(startPos.x, endPos.x),
                                                    Screen.height - Mathf.Max(startPos.y, endPos.y),
                                                    Mathf.Abs(startPos.x - endPos.x),
                                                    Mathf.Abs(startPos.y - endPos.y));
        GameObject[] slimeAObjects = GameObject.FindGameObjectsWithTag("SlimeA");

        foreach (var obj in slimeAObjects)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(obj.transform.position);
            if(selectionRect.Contains(screenPoint))
            {
                if(!selectedObjects.Contains(obj))
                {
                    selectedObjects.Add(obj);
                    obj.GetComponent<Renderer>().material.color = Color.yellow;
                }
            }
        }
    }
}
