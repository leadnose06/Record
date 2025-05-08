using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachOnEnter : MonoBehaviour
{
    public GameObject player;
    public GameObject grapple;
    float timer;
    void Awake()
    {
        
    }
    void Start()
    {
        player = GameObject.Find("Player");
        timer = 0.75f;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0){
            Destroy(gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.gameObject.tag == "Player"){
            player.GetComponent<GrapplingScript>().attached = grapple;
            player.GetComponent<GrapplingScript>().onConnect();
            Destroy(gameObject);
        }
    }
}
