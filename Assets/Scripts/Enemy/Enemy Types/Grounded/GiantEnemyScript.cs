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
        if(RB.velocity.x > 0){
            transform.localScale = new Vector2(-1, 1);
        }else if(RB.velocity.x < 0){
            transform.localScale = new Vector2(1, 1);
        }
        if(IsAggroed && Time.time >= lastAttack + attackInterval && StateMachine.CurrentEnemyState == EnemyChaseState){
            StateMachine.ChangeState(EnemyAttackState);
        }
    }
}
