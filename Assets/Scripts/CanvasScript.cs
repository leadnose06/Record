using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasScript : MonoBehaviour
{
    private int playerHealth;
    private int playerMaxHealth;
    private int playerNanos;
    private int playerMaxNanos;
    public GameObject player;
    public GameObject[] Hearts;
    public GameObject[] Nanos;
    public GameObject MainHeart;
    public GameObject MainNano;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public float energyLevel;
    public float maxEnergy;
    public Slider energySlider;
    public GameObject settings;


    // Start is called before the first frame update
    void Start()
    {
        //Get info from player's scripts
        player = GameObject.Find("Player");
        playerMaxHealth = DataManager.Instance.playerMaxHealth;
        playerHealth = DataManager.Instance.playerHealth;
        Debug.Log(playerHealth + " ");
        Debug.Log(playerMaxHealth + " ");

        playerNanos = 4;
        playerMaxNanos = 4;
        
        maxEnergy = DataManager.Instance.playerMaxEnergy;
        energyLevel = DataManager.Instance.playerEnergy;

        // Initialize UI setup
        Hearts = new GameObject[playerMaxHealth];
        Nanos = new GameObject[playerMaxNanos];
        //float x = MainHeart.transform.position.x;
        //float y = MainHeart.transform.position.y;
        float heartX = -300f;
        float heartY = 170f;
        Debug.Log("" + heartY);
       
        float nanoX = -294f;
        float nanoY = 125f;

        Hearts[0] = MainHeart;
        Nanos[0] = MainNano;
        //Instantiate Hearts
        for (int i = 1; i < Hearts.Length; i++) {
            Hearts[i] = Instantiate(MainHeart, new Vector3( heartX + i * 30, heartY, 0),Quaternion.identity);
            Debug.Log(i + " heart");
            Hearts[i].transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
        }
        //Instantiate Nanobots
         for (int i = 1; i < Nanos.Length; i++) {
            Nanos[i] = Instantiate(MainNano, new Vector3( nanoX + i * 30, nanoY, 0),Quaternion.identity);
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
        for (int i = Hearts.Length - 1; i >= 0 ; i--){
            if (i+1 > playerHealth) {
                Hearts[i].GetComponent<Image>().sprite = emptyHeart;
            }
        }
    }

    public void onMenu(){
        settings.SetActive(true);
    }
    public void onCancel(){
        if(settings.activeSelf){
            settings.SetActive(false);
        }
    }

    
}
