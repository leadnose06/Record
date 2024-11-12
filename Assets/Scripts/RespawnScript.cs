using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [SerializeField]
    private GameObject lastSafeZone;

    public Transform getRespawn{
        get { return lastSafeZone.transform.GetChild(0).transform;}
    }
    
    void OnTriggerEnter2D(Collider2D collision){
        if(gameObject.GetComponent<TouchingDirections>().isGrounded){
            if(collision.tag.Equals("Respawn")){
                lastSafeZone = collision.gameObject;
            }
        }
    }
}
