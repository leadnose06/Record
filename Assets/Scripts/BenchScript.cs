using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchScript : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        if(DataManager.Instance.dead){
            player.transform.position = transform.position;
            DataManager.Instance.playerHealth = DataManager.Instance.playerMaxHealth;
            player.GetComponent<Health>().health = DataManager.Instance.playerMaxHealth;
            DataManager.Instance.dead = false;
        }
    }



}
