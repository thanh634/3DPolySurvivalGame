using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public void BackToMainMenu() {
        Debug.Log("Returning to main menu...");
        // Load the main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
