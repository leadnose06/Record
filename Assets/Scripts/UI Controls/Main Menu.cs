using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject main;
    public GameObject settings;
    public GameObject saves;


    public void onPlay(){
        DataManager.Instance.CreateSave1();
        DataManager.Instance.LoadGame();
        if(DataManager.Instance.playerData.lastBench != null){
            SceneManager.LoadScene(DataManager.Instance.playerData.lastBench);
        }else{
            DataManager.Instance.toBench = false;
            SceneManager.LoadScene("Opening");
        }
    }
    public void onPlay2(){
        DataManager.Instance.CreateSave2();
        DataManager.Instance.LoadGame();
        if(DataManager.Instance.playerData.lastBench != null){
            SceneManager.LoadScene(DataManager.Instance.playerData.lastBench);
        }else{
            DataManager.Instance.toBench = false;
            SceneManager.LoadScene("Opening");
        }
    }
    public void onPlay3(){
        DataManager.Instance.CreateSave3();
        DataManager.Instance.LoadGame();
        if(DataManager.Instance.playerData.lastBench != null){
            SceneManager.LoadScene(DataManager.Instance.playerData.lastBench);
        }else{
            DataManager.Instance.toBench = false;
            SceneManager.LoadScene("Continuity Testing");
        }
    }
    public void onSettings(){
        main.SetActive(false);
        settings.SetActive(true);
    }
    public void onSaves(){
        main.SetActive(false);
        saves.SetActive(true);
    }
    public void onBack(){
        if(settings.activeSelf){
            settings.SetActive(false);
            main.SetActive(true);
        }
    }
}
