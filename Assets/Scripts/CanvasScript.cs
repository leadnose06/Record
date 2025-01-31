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
    public GameObject energyBar;
    public int energyLevel = 0;
    private Vector3 energyBarSize;


    // Start is called before the first frame update
    void Start()
    {
        //Get info from player's scripts
        player = GameObject.Find("Player");
        playerMaxHealth = player.GetComponent<Health>().GetMaxHealth();
        playerHealth = player.GetComponent<Health>().GetCurrentHealth();
        Debug.Log(playerHealth + " ");
        Debug.Log(playerMaxHealth + " ");

        playerNanos = 4;
        playerMaxNanos = 4;

        energyLevel = 5;
        energyBarSize = energyBar.GetComponent<RectTransform>().localScale;
        energyBar.GetComponent<RectTransform>().localScale = energyBarSize * 0.1f * energyLevel;
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
        energyBar.GetComponent<RectTransform>().localScale = energyBarSize * 0.1f * energyLevel;
    }

    // Update is called once per frame
    void Update()
    {
        playerMaxHealth = player.GetComponent<Health>().GetMaxHealth();
        playerHealth = player.GetComponent<Health>().GetCurrentHealth();
        for (int i = Hearts.Length - 1; i >= 0 ; i--){
            if (i+1 > playerHealth) {
                Hearts[i].GetComponent<Image>().sprite = emptyHeart;
            }
        }
    }

    
}
