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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        timer = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0){
            Destroy(gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        player.GetComponent<GrapplingScript>().attached = grapple;
        player.GetComponent<GrapplingScript>().onConnect();
    }
}
