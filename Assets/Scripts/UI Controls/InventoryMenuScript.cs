using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuScript : MonoBehaviour
{
    public GameObject inventory;
    public GameObject map;
    private bool inMap;
void Awake()
    {
        inMap = false;
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        if(inMap){
            map.SetActive(true);
            inventory.SetActive(false);
        } else {
            inventory.SetActive(true);
            map.SetActive(false);
        }
        transform.SetAsLastSibling();
    }
    
    public void onBack(){
        inventory.SetActive(false);
        map.SetActive(false);
    }

    //TODO: change for more than two tabs
    public void onLeft(){
        if(gameObject.activeSelf){
            if(inMap){
                inMap = false;
                inventory.SetActive(true);
                map.SetActive(false);
            } else{
                inMap = true;
                map.SetActive(true);
                inventory.SetActive(false);
            }
        }
    }
}
