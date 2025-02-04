using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject main;
    public GameObject settings;


    public void onPlay(){
        //TODO: switch to save select
        SceneManager.LoadScene("Continuity Testing");
    }
    public void onSettings(){
        main.SetActive(false);
        settings.SetActive(true);
    }
    public void onBack(){
        if(settings.activeSelf){
            settings.SetActive(false);
            main.SetActive(true);
        }
    }
}
