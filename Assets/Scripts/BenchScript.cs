using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchScript : MonoBehaviour
{
    public GameObject player;
    void Awake()
    {
        DataManager.Instance.bench = gameObject;
        if(DataManager.Instance.dead){
            respawn();
        }
    }

    public void respawn(){
        player.transform.position = transform.position;
        DataManager.Instance.playerHealth = DataManager.Instance.playerMaxHealth;
        player.GetComponent<Health>().health = DataManager.Instance.playerMaxHealth;
        DataManager.Instance.dead = false;
    }



}
