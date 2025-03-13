using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Chase-Ground Chase", menuName = "Enemy Logic/Chase Logic/Ground Chase")]
public class EnemyGroundChase : EnemyChaseSOBase
{
    public GameObject player;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("Chase");
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
        if(Vector3.Distance(enemy.transform.position, player.transform.position) > 7f || Physics2D.Raycast(enemy.transform.position, player.gameObject.transform.position - enemy.transform.position, Mathf.Infinity, enemy.contactFilter.layerMask).collider.tag != "Player" || Vector2.Distance(enemy.origin, new Vector2(transform.position.x, transform.position.y)) > enemy.maxDist){
            enemy.SetAggroStatus(false);
            enemy.StateMachine.ChangeState(enemy.EnemyIdleState);
        } else {
            enemy.RB.velocity = new Vector2(enemy.speed * Mathf.Sign(player.transform.position.x - enemy.transform.position.x), 0);
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
