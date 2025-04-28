using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rb;
    private Collider2D c;
    public float speed = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            player.GetComponent<PlayerController>().Knockback(false, 1 ,collision);
            player.GetComponent<PlayerColliderScript>().SetInvulnerable();
        }
    }

}
