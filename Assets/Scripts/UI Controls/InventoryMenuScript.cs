using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuScript : MonoBehaviour
{
    public GameObject inventory;
    public GameObject map;
    private bool inMap;
    public Sprite noAbility;
    public Sprite dashSprite;
    public Sprite grappleSprite;
    public Sprite jumpSprite;
    public Button dashButton;
    public Button grappleButton;
    public Button jumpButton;
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
        if(DataManager.Instance.playerData.dash){
            dashButton.image.sprite = dashSprite;
        } else{
            dashButton.image.sprite = noAbility;
        }
        if(DataManager.Instance.playerData.grapple){
            grappleButton.image.sprite = grappleSprite;
        } else{
            grappleButton.image.sprite = noAbility;
        }
        if(DataManager.Instance.playerData.doubleJump){
            jumpButton.image.sprite = jumpSprite;
        } else{
            jumpButton.image.sprite = noAbility;
        }
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
