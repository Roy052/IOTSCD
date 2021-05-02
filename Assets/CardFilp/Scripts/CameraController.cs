using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Stage_debug stage; //debug
    private Camera mainCamera;

    private float wDelta = 0.4f;
    private float hDelta = 0.6f;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    public void SetupCamera()
    {
        int width = stage.Width;
        int height = stage.Height;

        float size = (width>height) ? width*wDelta : height*hDelta;
        mainCamera.orthographicSize = size;

        if(height > width)
        {
            Vector3 position = new Vector3(0,0.05f,-10);
            position.y *= height;

            transform.position = position;
        }
    }

}
