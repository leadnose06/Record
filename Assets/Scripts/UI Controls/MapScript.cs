using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapScript : MonoBehaviour
{
    public ScrollRect scrollRect;
    void Awake(){
    }
    void OnEnable()
    {
        scrollRect.normalizedPosition = new Vector2(0.5f, 0.5f);
    }
    public void OnScroll(InputAction.CallbackContext context){
        if(context.performed){
            Gamepad gamepad = Gamepad.current;
            scrollRect.normalizedPosition += gamepad.leftStick.ReadValue()*0.1f;
        }

    }
}
