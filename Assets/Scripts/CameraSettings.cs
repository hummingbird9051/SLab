using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraSettings : MonoBehaviour
{
    [SerializeField] Camera camera;

    void Start()
    {
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0, 0, 0, 0);
        camera.allowHDR = false;
        camera.GetUniversalAdditionalCameraData().renderPostProcessing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
