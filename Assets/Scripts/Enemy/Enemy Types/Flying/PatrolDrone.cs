using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolDrone : FlyingBase
{
    public Collider2D aggroArea;
    public override void Start()
    {
        EnemyIdleBaseInstance.Initialize(gameObject, this, LeftPoint, RightPoint, speed);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(EnemyIdleState);

    }

    public void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){

            Debug.Log("1");
            RaycastHit2D results = Physics2D.Raycast(transform.position, collider.gameObject.transform.position - transform.position, Mathf.Infinity, contactFilter.layerMask);
            if(results.collider.tag == "Player"){
                SetAggroStatus(true);
                StateMachine.ChangeState(EnemyChaseState);
            }
        }
    }

}
