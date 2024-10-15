using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TileType { Empty=0, Base, Broke, Boom, Jump, StraightLeft, StraightRight, Blink, 
    ItemCoin =10,
    Player = 100}


public class Tile : MonoBehaviour
{
    [SerializeField]
    private Sprite[] tileImages;
    [SerializeField]
    private Sprite[] itemImages;
    [SerializeField]
    private Sprite playerImage;

    private TileType tileType;

    private SpriteRenderer spriteRenderer;

    public void Setup(TileType tileType)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        TileType = tileType; 
    }

    public TileType TileType
    {
        set
        {
            tileType = value;

            if( (int)tileType < (int)TileType.ItemCoin)
            {
                spriteRenderer.sprite = tileImages[(int)tileType];
            }
            else if( (int)tileType < (int)TileType.Player)
            {
                spriteRenderer.sprite = itemImages[(int)tileType - (int)TileType.ItemCoin];
            }
            else if((int)tileType == (int)TileType.Player)
            {
                spriteRenderer.sprite = playerImage;
            }
        }

        get => tileType;
    }
}
