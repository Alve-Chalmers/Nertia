using UnityEngine;

[ExecuteInEditMode]
public class SyncUICameraWithGame : MonoBehaviour
{
    public Camera pixelPerfectCamera;
    
    private Camera uiCamera;

    void Awake()
    {
        uiCamera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (pixelPerfectCamera == null)
        {
            return;
        }
        uiCamera.rect = pixelPerfectCamera.rect;
        uiCamera.orthographicSize = pixelPerfectCamera.orthographicSize;
    }
}
