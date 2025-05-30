using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class CanvasScript : MonoBehaviour
{
    private int playerHealth;
    private int playerMaxHealth;
    private int playerNanos;
    private int playerMaxNanos;
    public GameObject player;
    public List<GameObject> Hearts;
    public List<GameObject> Nanos;
    public GameObject MainHeart;
    public GameObject MainNano;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public float energyLevel;
    public float maxEnergy;
    public Slider energySlider;
    public GameObject menu;
    public GameObject inventoryMenu;
    public static bool paused = false;
    public InputActionAsset playerInput;



    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        //Get info from player's scripts
        player = GameObject.Find("Player");
        playerMaxHealth = DataManager.Instance.playerMaxHealth;
        playerHealth = DataManager.Instance.playerHealth;
        Debug.Log(playerHealth + " ");
        Debug.Log(playerMaxHealth + " ");

        playerNanos = DataManager.Instance.playerHeals;
        playerMaxNanos = DataManager.Instance.playerMaxHeals;
        
        maxEnergy = DataManager.Instance.playerMaxEnergy;
        energyLevel = DataManager.Instance.playerEnergy;

        // Initialize UI setup
        Hearts = new List<GameObject>(playerMaxHealth);
        Nanos = new List<GameObject>(0);
        //float x = MainHeart.transform.position.x;
        //float y = MainHeart.transform.position.y;
        float heartX = -300f;
        float heartY = 170f;
        Debug.Log("" + heartY);
       
        float nanoX = -294f;
        float nanoY = 125f;

        Hearts.Add(MainHeart);
        Nanos.Add(MainNano);
        //Instantiate Hearts
        for (int i = 1; i < playerMaxHealth; i++) {
            Hearts.Add(Instantiate(MainHeart, new Vector3( heartX + i * 30, heartY, 0),Quaternion.identity));
            Debug.Log(i + " heart");
            Hearts[i].transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
        }
        //Instantiate Nanobots
         for (int i = 1; i < playerMaxNanos; i++) {
            Nanos.Add(Instantiate(MainNano, new Vector3( nanoX + i * 30, nanoY, 0),Quaternion.identity));
            Debug.Log(i + " nano");
            Nanos[i].transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
        }
        //Set energy bar to correct level
        energyLevel = 5;
        energySlider.maxValue = maxEnergy;
        energySlider.value = energyLevel;
                
    }

    // Update is called once per frame
    void Update()
    {
        playerMaxHealth = DataManager.Instance.playerMaxHealth;
        playerHealth = DataManager.Instance.playerHealth;
        maxEnergy = DataManager.Instance.playerMaxEnergy;
        energyLevel = DataManager.Instance.playerEnergy;
        energySlider.value = energyLevel;
        //update hearts
        for (int i = Hearts.Count - 1; i >= 0 ; i--){
            if (i+1 > playerHealth) {
                Hearts[i].GetComponent<Image>().sprite = emptyHeart;
            }
            else {Hearts[i].GetComponent<Image>().sprite = fullHeart;}
        }
        //update nanos
        for (int i = Nanos.Count - 1; i >= 0 ; i--){
            if (i+1 > DataManager.Instance.playerHeals) {
                Nanos[i].GetComponent<Image>().enabled = false;
            }
            else {Nanos[i].GetComponent<Image>().enabled = true;}
        }
        
    }
    public void onHealButton(InputAction.CallbackContext context){
        if (context.performed)
        {
            if (DataManager.Instance.playerHeals > 0 && DataManager.Instance.playerHealth != DataManager.Instance.playerMaxHealth)
            {
                player.GetComponent<PlayerController>().onHeal(context);
            }
            else if (DataManager.Instance.playerMaxEnergy <= DataManager.Instance.playerEnergy && DataManager.Instance.playerHeals < DataManager.Instance.playerMaxHeals)
            {
                DataManager.Instance.playerHeals += 1;
                DataManager.Instance.playerEnergy = 0;
            }
        }
    }

    public void onMenu(InputAction.CallbackContext context){
        Debug.Log("stopped");
        if(!paused){
            if(playerInput == null) playerInput = context.action.actionMap.asset;
            playerInput.FindActionMap("Player").Disable();
            playerInput.FindActionMap("UI").Enable();
            menu.SetActive(true);
            Time.timeScale = 0;
            AudioListener.pause = true;
            paused = true;
        }
    }

    public void onInventory(InputAction.CallbackContext context){
        Debug.Log("Inventory Opened");
        if(context.performed &! paused){
            paused = true;
            if(playerInput == null) playerInput = context.action.actionMap.asset;
            playerInput.FindActionMap("Player").Disable();
            playerInput.FindActionMap("UI").Enable();
            inventoryMenu.SetActive(true);
        }
    }
    
    public void onCancel(InputAction.CallbackContext context){
        if(context.performed){
            if(menu.activeSelf){
                if(menu.GetComponent<PauseMenu>().onBack()){
                    menu.SetActive(false);
                    if(playerInput == null) playerInput = context.action.actionMap.asset;
                    playerInput.FindActionMap("Player").Enable();
                    Time.timeScale = 1;
                    AudioListener.pause = false;
                    paused = false;
                }
            } else if(inventoryMenu.activeSelf){
                inventoryMenu.SetActive(false);
                if(playerInput == null) playerInput = context.action.actionMap.asset;
                playerInput.FindActionMap("Player").Enable();
                paused = false;
            }
        }
    }
    public void onResumeButton(){
        menu.SetActive(false);
        playerInput.FindActionMap("Player").Enable();
        Time.timeScale = 1;
        AudioListener.pause = false;
        paused = false;
    }

    public void addHeart(){
        DataManager.Instance.playerMaxHealth++;
        DataManager.Instance.playerData.maxHealth++;
        DataManager.Instance.playerHealth = DataManager.Instance.playerMaxHealth;
        playerMaxHealth = DataManager.Instance.playerMaxHealth;
        float heartX = -300f;
        float heartY = 170f;
        Hearts.Add(Instantiate(MainHeart, new Vector3( heartX + (playerMaxHealth - 1) * 30, heartY, 0),Quaternion.identity));
        Hearts[playerMaxHealth - 1].transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

    }

    public void addNano(){
        DataManager.Instance.playerMaxHeals++;
        DataManager.Instance.playerData.maxHeals++;
        DataManager.Instance.playerHeals = DataManager.Instance.playerMaxHeals;
        playerMaxNanos = DataManager.Instance.playerMaxHeals;
        float nanoX = -294f;
        float nanoY = 125f;
        Nanos.Add(Instantiate(MainNano, new Vector3( nanoX + (playerMaxNanos - 1) * 30, nanoY, 0),Quaternion.identity));
        Nanos[playerMaxNanos - 1].transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

    }

    
}
