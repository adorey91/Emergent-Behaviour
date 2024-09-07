using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class UserInput : MonoBehaviour
{
    [SerializeField] Tilemap[] tilemaps;
    [SerializeField] Camera mainCamera;

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Pause or quit");
        }
    }

    public void SelectTile(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector3 worldPoint = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int selectedTilePosition = GetTileAtWorldPoint(worldPoint);

            if (selectedTilePosition != Vector3Int.zero)
            {
                Debug.Log("Selected tile position: " + selectedTilePosition);
            }
        }
    }

    private Vector3Int GetTileAtWorldPoint(Vector3 worldPoint)
    {
        foreach (var tilemap in tilemaps)
        {
            Vector3Int gridPosition = tilemap.WorldToCell(worldPoint);
            TileBase tile = tilemap.GetTile(gridPosition);

            if (tile != null) // If a tile exists at the clicked position in this tilemap
            {
                Debug.Log($"Selected Tile in {tilemap.name}: {gridPosition}");
                return gridPosition;
            }
        }

        return Vector3Int.zero; // Return a default value if no tile is found
    }
}