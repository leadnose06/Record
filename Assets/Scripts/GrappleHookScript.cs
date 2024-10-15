using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrappleHookScript : MonoBehaviour
{
    public GameObject player;
    //public Vector3 targetPos;
    public float targetDir;
    public float speed = 20f;
    void Awake()
    {
        //gameObject.SetActive(true);
        //targetDir = (targetPos - transform.position).normalized;
    }

    void Update()
    {
        transform.position += new Vector3(Mathf.Cos(Mathf.Deg2Rad*targetDir), Mathf.Sin(Mathf.Deg2Rad*targetDir)) * speed * Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D col){
        if(col.collider.tag != "Player"){
            if(col.collider.tag == "Grappleable" || col.collider.tag == "Enemy"){

            }else{
                Destroy(gameObject);
            }
        }
    }
}
