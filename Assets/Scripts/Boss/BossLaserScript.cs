using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserScript : MonoBehaviour
{
    public float duration;
    public bool spin;
    public bool clockwise;
    private float previewDuration;
    public float turnSpeed;

    void Start()
    {
        
    }
    void OnEnable()
    {
        gameObject.transform.localScale = new Vector3(100, 0.15f, 1);
        previewDuration = 0.45f;
        GetComponent<Collider2D>().enabled = false;
    }

    void Update()
    {
        previewDuration -= Time.deltaTime;
        if(previewDuration <= 0f){
            duration -= Time.deltaTime;
        }
        if(duration <= 0){
            gameObject.SetActive(false);
        }
        if(previewDuration <= 0 &! GetComponent<Collider2D>().enabled){
            gameObject.transform.localScale = new Vector3(100, 0.75f, 1);
            GetComponent<Collider2D>().enabled = true;
        }
        if(previewDuration <= 0 && spin){
            if(clockwise){
                transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
            }else{
                transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
            }
        }
    }

}
