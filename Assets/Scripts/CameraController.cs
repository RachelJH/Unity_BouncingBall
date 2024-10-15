using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private TileMap2D tilemap2D;
    private Camera mainCamera;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float zommSpeed;
    [SerializeField]
    private float minViewSize = 2;
    private float maxViewSize;

    private float wDelta = 0.4f;
    private float hDelta = 0.6f;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    public void SetupCamera()
    {
        int width = tilemap2D.Width;
        int height = tilemap2D.Height;

        float size = (width>height) ? width*wDelta : height*hDelta;

        mainCamera.orthographicSize = size;

        if(height > width)
        {
            Vector3 position = new Vector3(0, 0.05f, -10);
            position.y *= height;

            transform.position = position;
        }
        maxViewSize = mainCamera.orthographicSize;
    }

    public void SetPosition(float x, float y)
    {
        transform.position += new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;
    }

    public void SetOrthographicSize(float size)
    {
        if (size == 0) return;

        mainCamera.orthographicSize += size * zommSpeed * Time.deltaTime;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minViewSize, maxViewSize);
    }

}
