using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class GrappleHookScript : MonoBehaviour
{
    public GameObject player;
    public Vector3 targetPos;
    public float targetDir;
    public Vector3 Direction;
    public float speed = 3f;
    public bool target;
    Rigidbody2D triggerrb;
    public lr_LineController line;
    void Awake()
    {
        Direction = (targetPos - transform.position).normalized;
        triggerrb = GetComponent<Rigidbody2D>();
        if(!target){
            triggerrb.velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad*targetDir), Mathf.Sin(Mathf.Deg2Rad*targetDir)) * speed;
        } else{
            triggerrb.velocity = Direction * speed;

        }
    }

    void Update()
    {
    }

    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if(collision.tag != "Player"){
            if(collision.tag == "Grappleable" || collision.tag == "Enemy"){
                player.GetComponent<GrapplingScript>().attached = collision.gameObject;
                player.GetComponent<GrapplingScript>().onConnect();
                Destroy(gameObject);

            }else if(collision.tag == "Elite" || collision.tag == "Boss"){

            }else{
                Destroy(gameObject);
            }
        }
    }
}
