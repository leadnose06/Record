using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemyScript : FlyingBase
{

    public GameObject laser;
    public float timer;
    public float shootPeriod = 2f;
    public GameObject enemy;
    public GameObject player;
    

    public override void Start()
    {
        EnemyIdleBaseInstance.Initialize(gameObject, this, LeftPoint, RightPoint, speed);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(EnemyIdleState);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Update(){

        StateMachine.CurrentEnemyState.FrameUpdate();
        if (colorIsRed && colorHitTimer <= 0) {sprite.color = Color.white;}
        else {colorHitTimer -= Time.deltaTime;}

        timer += Time.deltaTime;

        if (timer >= shootPeriod){
                RaycastHit2D results = Physics2D.Raycast(transform.position, player.transform.position - transform.position, Mathf.Infinity, contactFilter.layerMask);
                if(results && IsAggroed){
                    if(results.collider.tag == "Player"){
                        timer = 0f;
                        shoot();
                    }
                }
        }
    }


    public void shoot(){
        Vector2 laserPos = new Vector2((enemy.transform.position.x - (0.2f*enemy.transform.localScale.x)), enemy.transform.position.y-0.15f);
        Instantiate(laser, laserPos, Quaternion.identity);
        Debug.Log("shot laser");
    }



}

