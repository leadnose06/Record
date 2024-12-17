using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolDrone : FlyingBase
{
    [SerializeField] private float speed;
    public override void Start()
    {
        EnemyIdleBaseInstance.Initialize(gameObject, this, LeftPoint, RightPoint, speed);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(EnemyIdleState);

    }

}
