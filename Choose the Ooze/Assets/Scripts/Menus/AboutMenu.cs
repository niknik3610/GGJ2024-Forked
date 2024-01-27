using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    public GameObject mainMenu;

    public GameObject aboutMenu;

    public void loadCreditsMenu(){
        mainMenu.SetActive(false);
        aboutMenu.SetActive(true);
    }

    public void exitCreditsMenu(){
        mainMenu.SetActive(true);
        aboutMenu.SetActive(false);
    }
}
