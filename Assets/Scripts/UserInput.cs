using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    [SerializeField] Tile tileScript;
    [SerializeField] Block blockScript;
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitblock;

            // Perform the raycast and check if it hits something
            if (Physics.Raycast(ray, out hitblock))
            {
                if (hitblock.collider.CompareTag("Land") || hitblock.collider.CompareTag("Water"))
                {
                    Block blockScript = hitblock.collider.gameObject.GetComponent<Block>();
                    if (blockScript != null)
                        blockScript.SetBlockType();
                }
            }

            // RaycastHit2D hitTile = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            // if (hitTile.collider.tag == "Land" || hitTile.collider.tag == "Water")
            // {
            // tileScript = hitTile.collider.gameObject.GetComponent<Tile>();
            // tileScript.SetTileType();
            // tileScript = null;
            // }
            else
                Debug.Log("leave the fish alone");
        }
    }
}

