using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button resume;
    public GameObject settings;
    public bool inSettings = false;
    public GameObject contents;
    void Awake()
    {
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (resume.gameObject);
    }
    public void onBack(){
        
    }

    public void onSettings(){
        contents.SetActive(false);
        settings.SetActive(true);
        inSettings = true;
    }

    public void onMain(){
        SceneManager.LoadScene("Main Menu");
    }
}
