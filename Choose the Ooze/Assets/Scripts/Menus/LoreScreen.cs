using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoreScreen : MonoBehaviour
{
    public GameObject textCard1;

    public GameObject textcard2;

    public CircleWipe circleWipe;

    void Awake(){   
        Debug.Log("Start lore screen");
        this.loadTextCard1();
    }

    public void loadTextCard1(){
        textCard1.SetActive(true);
        textcard2.SetActive(false);
    }

    public void loadTextCard2(){
        textCard1.SetActive(false);
        textcard2.SetActive(true);
    }

    public void loadMainGameScreen(){
        StartCoroutine(SceneTransition(2));
    }

    public IEnumerator SceneTransition(int sceneID)
    {
        circleWipe.Wipe();
        yield return new WaitForSeconds(circleWipe.transitionTime);
        SceneManager.LoadScene(sceneID);
    }
}
