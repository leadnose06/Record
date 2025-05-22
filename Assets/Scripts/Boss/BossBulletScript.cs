using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScripe : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rb;
    private Collider2D c;
    public float speed = 5.0f;
    public Vector3 direction;
    void OnEnable()
    {
        speed = 3.5f;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb.velocity = new Vector2(direction.x, direction.y) * speed;
    }
    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Player" && !DataManager.Instance.invulnerable)
        {
            player.GetComponent<PlayerController>().damage(1);
            player.transform.Find("EnemyCollider").gameObject.GetComponent<PlayerColliderScript>().SetInvulnerable();
            Debug.Log("hit");
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
    }

}
