using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantEnemyScript : Enemy
{
    [SerializeField] private float leftX;
    [SerializeField] private float rightX;
    [SerializeField] private float topY;
    [SerializeField] private float bottomY;
    [SerializeField] private float attackInterval;
    private float lastAttack = 0;

    public override void Start()
    {
        EnemyIdleBaseInstance.Initialize(gameObject, this, 1, speed, leftX, rightX, topY, bottomY);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(EnemyIdleState);
        if(IsFacingRight){
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }
    public override void Update()
    {
        base.Update();
        if(IsAggroed && Time.time >= lastAttack + attackInterval && StateMachine.CurrentEnemyState == EnemyChaseState){
            StateMachine.ChangeState(EnemyAttackState);
            lastAttack = Time.time;
        }
    }
    public override void CheckSight(Collider2D collider){
        RaycastHit2D results = Physics2D.Raycast(transform.position, collider.gameObject.transform.position - transform.position, Mathf.Infinity, contactFilter.layerMask);
        if(results){
            if(results.collider.tag == "Player" && StateMachine.CurrentEnemyState == EnemyIdleState){
                SetAggroStatus(true);
                StateMachine.ChangeState(EnemyChaseState);
                lastAttack = Time.time - 1f;
            }
        }
    }
}
