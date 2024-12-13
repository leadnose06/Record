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
    [SerializeField]private float speed = 5f;
    

    public override void Initialize(GameObject gameObject, Enemy enemy, Transform LeftPoint, Transform RightPoint){
        base.Initialize(gameObject, enemy, LeftPoint, RightPoint);
        this.LeftPoint = LeftPoint;
        Debug.Log("---------------"+this.LeftPoint.position.x);
        this.RightPoint = RightPoint;
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
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        Debug.Log(LeftPoint);
        if((enemy.transform.position - _targetPos).sqrMagnitude < 0.1f){
            if(rightPoint){
                _targetPos = LeftPoint.position;
                rightPoint = false;
            } else {
                _targetPos = RightPoint.position;
                rightPoint = true;
            }
        }
        _direction = (_targetPos - enemy.transform.position).normalized;
        enemy.MoveEnemy(_direction * speed);
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
