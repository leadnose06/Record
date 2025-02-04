using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public int playerHealth;
    public int playerMaxHealth;
    public bool dead = false;
    public string lastBench;
    public GameObject bench;
    public float playerMaxEnergy = 10;
    public float playerEnergy = 5;

    private void Awake(){
        if(Instance != null){
            Destroy(gameObject);
            return;
        }
        lastBench = SceneManager.GetActiveScene().name;
        Instance = this;
        DontDestroyOnLoad(gameObject);
        playerMaxHealth = 4;
        playerHealth = playerMaxHealth;
    }


}
