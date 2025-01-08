using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerBot : Enemy
{
    [SerializeField] private int wall = 1;
    [SerializeField] private float leftX;
    [SerializeField] private float rightX;
    [SerializeField] private float topY;
    [SerializeField] private float bottomY;

    public override void Start()
    {
        EnemyIdleBaseInstance.Initialize(gameObject, this, wall, speed, leftX, rightX, topY, bottomY);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(EnemyIdleState);
        if(IsFacingRight){
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }
}
