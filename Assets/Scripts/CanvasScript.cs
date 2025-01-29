using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    private int playerHealth;
    private int playerMaxHealth;
    public GameObject player;
    public GameObject[] Hearts;
    public GameObject MainHeart;


    // Start is called before the first frame update
    void Start()
    {
        //Get info from player's scripts
        player = GameObject.Find("Player");
        playerMaxHealth = player.GetComponent<Health>().GetMaxHealth();
        playerHealth = player.GetComponent<Health>().GetCurrentHealth();

        // Initialize UI setup
        Hearts = new GameObject[playerMaxHealth];
        float x = MainHeart.transform.position.x;
        float y = MainHeart.transform.position.y;
        Hearts[0] = MainHeart;
        for (int i = 1; i < Hearts.Length; i++) {
            Hearts[i] = Instantiate(MainHeart, new Vector3( x + i * 30, y, 0),Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerMaxHealth = player.GetComponent<Health>().GetMaxHealth();
        playerHealth = player.GetComponent<Health>().GetCurrentHealth();
    }
}
