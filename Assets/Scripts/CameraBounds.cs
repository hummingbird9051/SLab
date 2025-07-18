using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraBounds : MonoBehaviour
{
    private Transform topWall, bottomWall, leftWall, rightWall;

    public PhysicsMaterial2D noBouncinessMaterial2D;

    void Start()
    {
        CreateBounds();
        SetBoundsPositions();
    }

    void CreateBounds()
    {
        topWall = new GameObject("TopWall").transform;
        bottomWall = new GameObject("BottomWall").transform;
        leftWall = new GameObject("LeftWall").transform;
        rightWall = new GameObject("RightWall").transform;

        topWall.SetParent(transform);
        bottomWall.SetParent(transform);
        leftWall.SetParent(transform);
        rightWall.SetParent(transform);

        topWall.gameObject.AddComponent<BoxCollider2D>();
        bottomWall.gameObject.AddComponent<BoxCollider2D>();
        leftWall.gameObject.AddComponent<BoxCollider2D>();
        rightWall.gameObject.AddComponent<BoxCollider2D>();

        bottomWall.GetComponent<BoxCollider2D>().sharedMaterial = noBouncinessMaterial2D;
    }

    void SetBoundsPositions()
    {
        Camera cam = GetComponent<Camera>();

        float cameraHeight = cam.orthographicSize * 2;
        float cameraWidth = cameraHeight * cam.aspect;

        topWall.localPosition = new Vector3(0, cameraHeight / 2 + 0.15f, 0);
        bottomWall.localPosition = new Vector3(0, -cameraHeight / 2 - 0.15f, 0);
        leftWall.localPosition = new Vector3(-cameraWidth / 2 - 0.15f, 0, 0);
        rightWall.localPosition = new Vector3(cameraWidth / 2 + 0.15f, 0, 0);

        topWall.GetComponent<BoxCollider2D>().size = new Vector2(cameraWidth + 1f, .3f);
        bottomWall.GetComponent<BoxCollider2D>().size = new Vector2(cameraWidth + 1f, .3f);
        leftWall.GetComponent<BoxCollider2D>().size = new Vector2(.3f, cameraHeight + 1f);
        rightWall.GetComponent<BoxCollider2D>().size = new Vector2(.3f, cameraHeight + 1f);
    }
}