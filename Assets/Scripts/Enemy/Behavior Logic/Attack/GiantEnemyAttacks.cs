using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Giant Enemy Attack", menuName = "Enemy Logic/Attack Logic/Giant Enemy Attack")]
public class GiantEnemyAttacks : EnemyAttackSOBase
{
    private bool jumping = false;
    private float prevVelocity;
    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("jumptrigger");
        prevVelocity = enemy.RB.velocity.y;
        if(Vector2.Distance(playerTransform.position, transform.position) > 1 &! jumping){
            enemy.RB.velocity = new Vector2(-4f*transform.localScale.x, 10);
            enemy.RB.gravityScale = 3;
            jumping = true;
        }
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if(enemy.RB.velocity.y == 0 && enemy.RB.velocity.y == prevVelocity && jumping){
            jumping = false;
            enemy.StateMachine.ChangeState(enemy.EnemyChaseState);
        }
        prevVelocity = enemy.RB.velocity.y;
    }
    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }
    public override void ResetValues()
    {
        base.ResetValues();
    }
}
