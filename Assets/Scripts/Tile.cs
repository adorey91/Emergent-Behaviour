using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType { Nothing, Land, Water }
    public TileType tileType;
    private TileType currentType;
    [SerializeField] private Sprite landSprite;
    [SerializeField] private Sprite waterSprite;
    

    private void Start()
    {
        currentType = tileType;

        if (currentType == TileType.Nothing)
            Debug.Log(gameObject.name + " set to nothing");
    }

    /// <summary>  Changes the tile type when selected  </summary>
    public void SetTileType()
    {
        tileType = currentType == TileType.Land ? TileType.Water : TileType.Land;
        currentType = tileType;
        
        UpdateAppearance();
    }

    /// <summary>  Function that updates tile appearance based on tile type </summary> 
    private void UpdateAppearance()
    {
        if(tileType == TileType.Nothing) return;
        SwitchTile(tileType);
    }

    private void SwitchTile(TileType newType)
    {
        Actions.OnPlaySFX?.Invoke("Tile");
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        switch(newType)
        {
            case TileType.Land:
                spriteRenderer.sprite = landSprite;
                gameObject.tag = "Land";
                gameObject.layer = LayerMask.NameToLayer("Land");
                break;
            case TileType.Water:
                spriteRenderer.sprite = waterSprite;
                gameObject.tag = "Water";
                gameObject.layer = LayerMask.NameToLayer("Water");
                break;
        }
    }
}
