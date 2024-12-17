using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerBot : Enemy
{
    [SerializeField] private Collider2D FloorCheck;
    [SerializeField] private Collider2D WallCheck;
    public override void Start()
    {
        EnemyIdleBaseInstance.Initialize(gameObject, this, 1, FloorCheck, WallCheck);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(EnemyIdleState);
        if(IsFacingRight){
            transform.localScale = new Vector3(1, 1, -1);
        }

    }
}
