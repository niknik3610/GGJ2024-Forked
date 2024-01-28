using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject aboutMenu;
    public CircleWipe circleWipe;
    void Awake(){   
        Debug.Log("Start main menu");
        gameObject.SetActive(true);
        aboutMenu.SetActive(false);
    }

    public void LaunchAboutMenu(){
        Debug.Log("Start about menu");
        aboutMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void LaunchGame(){
        StartCoroutine(SceneTransition(1));
    }
 
    public void QuitGame(){
        Debug.Log("Quit game");
        Application.Quit();
    }

    public IEnumerator SceneTransition(int sceneID)
    {
        circleWipe.Wipe();
        yield return new WaitForSeconds(circleWipe.transitionTime);
        SceneManager.LoadScene(sceneID);
    }
}
