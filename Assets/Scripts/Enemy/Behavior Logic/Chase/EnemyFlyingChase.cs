using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-FlyingChase", menuName = "Enemy Logic/Chase Logic/Flying Chase")]
public class EnemyFlyingChase : EnemyChaseSOBase
{
    public GameObject player;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        player = GameObject.FindWithTag("Player");

    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if(Vector3.Distance(transform.position, player.transform.position) > 10 || Physics2D.Raycast(transform.position, player.gameObject.transform.position - transform.position, Mathf.Infinity, enemy.contactFilter.layerMask).collider.tag != "Player" || Vector2.Distance(enemy.origin, new Vector2(transform.position.x, transform.position.y)) > enemy.maxDist){
            enemy.SetAggroStatus(false);
            enemy.StateMachine.ChangeState(enemy.EnemyIdleState);
        } else {
            Vector2 _direction = (player.transform.position - enemy.transform.position).normalized;
            enemy.MoveEnemy(_direction * enemy.speed);
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
