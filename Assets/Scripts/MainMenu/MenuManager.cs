using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; set; }

    public GameObject menuCanvas;
    public GameObject uiCanvas;
    public GameObject saveMenu;
    public GameObject settingMenu;
    public GameObject menu;

    public bool isMenuOpen = false;
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.M) && !isMenuOpen) {
            uiCanvas.SetActive(false);
            menuCanvas.SetActive(true);
            isMenuOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // SelectionManager.instance.DisableSelection();
            SelectionManager.instance.GetComponent<SelectionManager>().enabled = false;

        } else if (Input.GetKeyDown(KeyCode.M) && isMenuOpen) {
            saveMenu.SetActive(false);
            settingMenu.SetActive(false);
            menu.SetActive(true);
            
            uiCanvas.SetActive(true);
            menuCanvas.SetActive(false);
            isMenuOpen = false;
        
            if (CraftingSystem.instance.isOpen == false && InventorySystem.instance.isOpen == false) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            // SelectionManager.instance.EnableSelection();
            SelectionManager.instance.GetComponent<SelectionManager>().enabled = true;
            
        }

    }
}
