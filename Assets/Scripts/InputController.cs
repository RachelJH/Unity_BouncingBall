using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    private TileType currentType = TileType.Empty;
    private Tile playerTile = null;

    [SerializeField]
    private CameraController cameraController;
    private Vector2 previousMousePosition;
    private Vector2 currentMousePosition;

    private void Update()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;

        UpdateCamera();
        RaycastHit hit;

        if(Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Tile tile = hit.transform.GetComponent<Tile>();
                if (tile != null)
                {
                    if(currentType == TileType.Player)
                    {
                        if(playerTile != null)
                        {
                            playerTile.TileType = TileType.Empty;
                        }
                        playerTile = tile;
                    }

                    tile.TileType = currentType;
                }
            }
        }
    }
    public void SetTileType(int tileType)
    {
        currentType = (TileType)tileType;
    }

    public void UpdateCamera()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        cameraController.SetPosition(x, y);

        if( Input.GetMouseButtonDown(2))
        {
            currentMousePosition = previousMousePosition = Input.mousePosition;
        }
        else if(Input.GetMouseButton(2))
        {
            currentMousePosition = Input.mousePosition;
            if(previousMousePosition != currentMousePosition)
            {
                Vector2 move = (previousMousePosition - currentMousePosition) * 0.5f;
                cameraController.SetPosition(move.x, move.y);
            }
        }
        previousMousePosition = currentMousePosition;

        float distance = Input.GetAxisRaw("Mouse ScrollWheel");
        cameraController.SetOrthographicSize(-distance);
    }
}
