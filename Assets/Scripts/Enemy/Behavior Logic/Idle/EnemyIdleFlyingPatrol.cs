using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Flying Patrol", menuName = "Enemy Logic/Idle Logic/Flying Patrol")]
public class EnemyIdleFlyingPatrol : EnemyIdleSOBase
{
    public Transform LeftPoint;
    public Transform RightPoint;
    //public float VariancePercent = 15;
    private Vector3 _targetPos;
    private Vector3 _direction;
    private bool rightPoint;
    private float speed = 1.5f;
    private bool returning = false;
    

    public override void Initialize(GameObject gameObject, Enemy enemy, Transform LeftPoint, Transform RightPoint, float speed){
        base.Initialize(gameObject, enemy, LeftPoint, RightPoint, speed);
        this.LeftPoint = LeftPoint;
        this.RightPoint = RightPoint;
        this.speed = speed;
    }
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        if(Random.Range(0,1) <= 0.5){
            _targetPos = LeftPoint.position;
            rightPoint = false;
        } else{
            _targetPos = RightPoint.position;
            rightPoint = true;
        }
        if(Vector2.Distance(enemy.origin, new Vector2(enemy.transform.position.x, enemy.transform.position.y)) > 1){
            returning = true;
            Debug.Log("returning");
        }
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if(!returning){
            if((_targetPos - enemy.transform.position).sqrMagnitude < 0.2f){
                if(rightPoint){
                    _targetPos = LeftPoint.position;
                    rightPoint = false;
                    //Debug.Log("targeting left point");
                } else {
                    _targetPos = RightPoint.position;
                    rightPoint = true;
                    //Debug.Log("targeting right point");
                }
            }
            _direction = (_targetPos - enemy.transform.position).normalized;
            enemy.MoveEnemy(_direction * speed);
            Debug.Log("Moving, Distance: " + (_targetPos - enemy.transform.position).sqrMagnitude);
        } else{
            if(Vector2.Distance(enemy.origin, new Vector2(enemy.transform.position.x, enemy.transform.position.y)) < 1){
                returning = false;
                //Debug.Log("not returning");
            } else{
                _direction = (new Vector3(enemy.origin.x, enemy.origin.y) - enemy.transform.position).normalized;
                enemy.MoveEnemy(_direction * speed);
                //Debug.Log("Direction Changed");
            }
        }
    }
    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }
    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }
    public override void ResetValues()
    {
        base.ResetValues();
    }
}
