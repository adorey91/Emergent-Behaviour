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
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip sfxClip;

    private void Start()
    {
        GameObject sfxSound;
        sfxSound = GameObject.Find("SFX Sound");
        if (sfxSound == null)
            Debug.Log("Cant find sfx Sound");
        sfxSource = sfxSound.GetComponent<AudioSource>();
        currentType = tileType;
        //UpdateAppearance();

        if (currentType == TileType.Nothing)
            Debug.Log(gameObject.name + " set to nothing");
    }

    /// <summary>  Changes the tile type when selected  </summary>
    public void SetTileType()
    {
        if (currentType == TileType.Land)
            tileType = TileType.Water;
        else if (currentType == TileType.Water)
            tileType = TileType.Land;

        currentType = tileType;
        UpdateAppearance();
    }

    /// <summary>  Function that updates tile appearance based on tile type </summary> 
    private void UpdateAppearance()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (sfxSource == null)
        {
            GameObject sfxSound;
            sfxSound = GameObject.Find("SFX Sound");
            sfxSource = sfxSound.GetComponent<AudioSource>();
        }
        switch (tileType)
        {
            case TileType.Land:
                sfxSource.PlayOneShot(sfxClip);
                spriteRenderer.sprite = landSprite;
                gameObject.tag = "Land";
                gameObject.layer = LayerMask.NameToLayer("Land");
                break;
            case TileType.Water:
                sfxSource.PlayOneShot(sfxClip);
                spriteRenderer.sprite = waterSprite;
                gameObject.tag = "Water";
                gameObject.layer = LayerMask.NameToLayer("Water");
                break;
            case TileType.Nothing: break;
        }
    }
}
