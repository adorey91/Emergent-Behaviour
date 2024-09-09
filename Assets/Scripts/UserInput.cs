using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    [SerializeField] Tile tileScript;
    [SerializeField] Menu pauseMenu;

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
            RaycastHit2D hitTile = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hitTile.collider.tag == "Land" || hitTile.collider.tag == "Water")
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
