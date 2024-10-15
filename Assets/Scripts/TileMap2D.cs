using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileMap2D : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private TMP_InputField inputWidth;
    [SerializeField]
    private TMP_InputField inputHeight;
    public int Width { private set; get; } = 10;
    public int Height { private set; get; } = 10;

    private void Awake()
    {
        inputWidth.text = Width.ToString();
        inputHeight.text = Height.ToString();

        //GenerateTilemap();
    }

    public void GenerateTilemap()
    {
        int width;
        int height;

        int.TryParse(inputWidth.text, out width);
        int.TryParse(inputHeight.text, out height);

        Width = width;
        Height = height; 

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Vector3 position = new Vector3((-Width * 0.5f + 0.5f) + x, (Height * 0.5f - 0.5f) - y, 0);

                SpawnTile(TileType.Empty, position);
            }
        }
    }
    
    private void SpawnTile(TileType tileType, Vector3 position)
    {
        GameObject clone = Instantiate(tilePrefab, position, Quaternion.identity);

        clone.name = "Tile";
        clone.transform.SetParent(transform);

        Tile tile = clone.GetComponent<Tile>();
        tile.Setup(tileType);
    }
}
