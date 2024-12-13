using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolDrone : FlyingBase
{
    
    public override void Start()
    {
        EnemyIdleBaseInstance.Initialize(gameObject, this, LeftPoint, RightPoint);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(EnemyIdleState);

    }

}
