using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnMenu : MonoBehaviour
{
    public void Respawn()
    {


        MenuManager.Instance.uiCanvas.SetActive(true);
        MenuManager.Instance.menuCanvas.SetActive(false);
        MenuManager.Instance.isMenuOpen = false;


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        // SelectionManager.instance.EnableSelection();
        SelectionManager.instance.GetComponent<SelectionManager>().enabled = true;

        PlayerState.instance.Respawn();
    }
    public void NewGame()
    {
        Debug.Log("Starting new game...");
        SceneManager.LoadScene("Environment_Free");
    }

    public void ExitGame()
    {
        Debug.Log("Back to main menu...");
        SceneManager.LoadScene("MainMenu");
    }
}
