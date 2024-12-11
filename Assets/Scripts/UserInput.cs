using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    [SerializeField] Tile tileScript;
    [SerializeField] Menu pauseMenu;

    [SerializeField] LayerMask tileLayerMask;
    RaycastHit2D hitTile;


    private void Start()
    {
        tileScript = null;
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
            pauseMenu.EscapeState();
    }

    public void SelectTile(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("UI is blocking the raycast");
                return;
            }

            // Perform the raycast and check if it hits something in the specified LayerMask
            RaycastHit2D hitTile = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileLayerMask);
            if (hitTile.collider != null)
            {
                tileScript = hitTile.collider.gameObject.GetComponent<Tile>();
                tileScript.SetTileType();
                tileScript = null;
            }
            else
                Debug.Log("leave the fish alone");
        }
    }
}

