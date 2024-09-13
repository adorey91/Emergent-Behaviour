using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BlockType { Nothing, Land, Water, Boundaries }
    public BlockType blockType;
    private BlockType currentType;

    [SerializeField] private Material landMat;
    [SerializeField] private Material waterMat;

    private void Start()
    {
        currentType = blockType;

        if (currentType == BlockType.Nothing)
            Debug.Log(gameObject.name + " set to nothing");
    }

    public void SetBlockType()
    {
        switch (currentType)
        {
            case BlockType.Land: blockType = BlockType.Water; break;
            case BlockType.Water: blockType = BlockType.Land; break;
        }

        currentType = blockType;
        UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        switch (blockType)
        {
            case BlockType.Land:
                meshRenderer.material = landMat;
                gameObject.tag = "Land";
                gameObject.layer = LayerMask.NameToLayer("Land");
                break;
            case BlockType.Water:
                meshRenderer.material = waterMat;
                gameObject.tag = "Water";
                gameObject.layer = LayerMask.NameToLayer("Water");
                break;
        }
    }
}
