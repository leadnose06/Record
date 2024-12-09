using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    private Vector3 _targetPos;
    private Vector3 _direction;
    private bool rightPoint;
    /*private float xVariance;
    private float yVariance;
    float maxSpeedChange;*/
        public PatrolState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        /*maxSpeedChange = (enemy.speed/100)*enemy.VariancePercent;
        xVariance = Random.Range(-1, 1);
        yVariance = Random.Range(-1, 1);*/

        if(Random.Range(0,1) <= 0.5){
            _targetPos = enemy.LeftPoint.position;
            rightPoint = false;
        } else{
            _targetPos = enemy.RightPoint.position;
            rightPoint = true;
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if((enemy.transform.position - _targetPos).sqrMagnitude < 0.1f){
            if(rightPoint){
                _targetPos = enemy.LeftPoint.position;
                rightPoint = false;
            } else {
                _targetPos = enemy.RightPoint.position;
                rightPoint = true;
            }
        }
        _direction = (_targetPos - enemy.transform.position).normalized;
        enemy.MoveEnemy(_direction * enemy.speed);

        //enemy.RB.velocity
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
