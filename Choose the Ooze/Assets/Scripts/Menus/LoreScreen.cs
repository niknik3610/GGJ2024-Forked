using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoreScreen : MonoBehaviour
{
    public GameObject textCard1;

    public GameObject textcard2;

    void Awake(){   
        Debug.Log("Start lore screen");
        textCard1.SetActive(true);
        textcard2.SetActive(false);
    }

    public void loadTextCard1(){
        textCard1.SetActive(true);
        textcard2.SetActive(false);
    }

    public void loadTextCard2(){
        textCard1.SetActive(false);
        textcard2.SetActive(true);
    }
}
