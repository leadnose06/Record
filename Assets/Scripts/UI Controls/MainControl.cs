using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainControl : MonoBehaviour
{
    public Button play;
    void OnEnable(){
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (play.gameObject);
    }


}
