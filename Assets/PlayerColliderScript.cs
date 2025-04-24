using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerColliderScript : MonoBehaviour
{
    // Start is called before the first frame update
public GameObject player;
public bool invulnerable = false;
private float invulnerableTimer = 0.25f;
public SpriteRenderer sprite;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (invulnerable){
            invulnerableTimer -= Time.deltaTime;
        }
        if (invulnerableTimer <= 0f) {
            invulnerable = false;
            invulnerableTimer = 0.25f;
            Debug.Log("No Longer Invulnerable");
            sprite.color = Color.white;
        }
    }

    public void SetInvulnerable(){
        if (!invulnerable){
            invulnerable = true;
            invulnerableTimer = 1.5f;
            Debug.Log("Invulnerable");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collider triggered");
        if(collision.gameObject.tag == "Enemy"){
            if (!invulnerable){
                int damageAmount = 1;
                //int damageAmount = collision.gameObject.getComponent<Enemy>().getDamageAmount();
                if (collision.gameObject.transform.position.x > transform.position.x){
                    player.GetComponent<PlayerController>().Knockback(true,damageAmount,collision);
                }
                else{
                    player.GetComponent<PlayerController>().Knockback(false,damageAmount,collision);
                }
                invulnerable = true;
                invulnerableTimer = 1.5f;
                Debug.Log("Invulnerable");
            }
        }

        
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy"){
            if (!invulnerable){
                int damageAmount = 1;
                //int damageAmount = collision.gameObject.getComponent<Enemy>().getDamageAmount();
                if (collision.gameObject.transform.position.x > transform.position.x){
                    player.GetComponent<PlayerController>().Knockback(true,damageAmount,collision);
                }
                else{
                    player.GetComponent<PlayerController>().Knockback(false,damageAmount,collision);
                }
                invulnerable = true;
                invulnerableTimer = 1.5f;
                Debug.Log("Invulnerable");
            }
        }
    }
}
