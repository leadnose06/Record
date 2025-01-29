using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStart(InputAction.CallbackContext context){
        if(context.performed){
            //TODO: switch to save select
            SceneManager.LoadScene("Demo Scene 0");
        }
    }
}
