using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void EscapeState()
    {
        if (pauseMenu.activeSelf)
            pauseMenu.SetActive(false);
        else
            pauseMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
