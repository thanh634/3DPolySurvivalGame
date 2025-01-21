using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame() {
        Debug.Log("Starting new game...");
        SceneManager.LoadScene("Environment_Free");
    }

    public void ExitGame() {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
