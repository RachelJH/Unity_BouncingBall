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

    private MapData mapData;
    public int Width { private set; get; } = 10;
    public int Height { private set; get; } = 10;

    public List<Tile> TileList { private set; get; }
    private void Awake()
    {
        inputWidth.text = Width.ToString();
        inputHeight.text = Height.ToString();

        mapData = new MapData();
        TileList = new List<Tile>();

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

        mapData.mapSize.x = Width;
        mapData.mapSize.y = Height;
        mapData.mapData = new int[TileList.Count];
    }
    
    private void SpawnTile(TileType tileType, Vector3 position)
    {
        GameObject clone = Instantiate(tilePrefab, position, Quaternion.identity);

        clone.name = "Tile";
        clone.transform.SetParent(transform);

        Tile tile = clone.GetComponent<Tile>();
        tile.Setup(tileType);
    
        TileList.Add(tile);
    }

    public MapData GetMapData()
    {
        for (int i = 0; i < TileList.Count; i++)
        {
            if (TileList[i].TileType != TileType.Player)
            {
                mapData.mapData[i] = (int)TileList[i].TileType;
            }
            else
            {
                mapData.mapData[i] = (int)TileType.Empty;

                int x = (int)TileList[i].transform.position.x;
                int y = (int)TileList[i].transform.position.y;
                mapData.playerPosition = new Vector2Int(x, y);
            }
        }
        return mapData;
    }
}
