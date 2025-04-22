using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public Button first;
    void Awake()
    {
    }
    void OnEnable()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (first.gameObject);
        transform.SetAsLastSibling();
    }
}
