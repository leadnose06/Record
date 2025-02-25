using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenuScript : MonoBehaviour
{
    public Button save1;
    void Awake()
    {
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (save1.gameObject);
        transform.SetAsLastSibling();
    }
}
