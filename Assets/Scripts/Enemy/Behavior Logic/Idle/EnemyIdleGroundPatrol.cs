using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Idle-Ground Patrol", menuName = "Enemy Logic/Idle Logic/Ground Patrol")]
public class EnemyIdleGroundPatrol : EnemyIdleSOBase
{
    private int wall;
    private float leftX;
    private float rightX;
    private float topY;
    private float bottomY;
    private bool right;
    private float speed = 0.75f;
    private bool goright;
    public override void Initialize(GameObject gameObject, Enemy enemy, int wall, float speed, float leftX, float rightX, float topY, float bottomY)
    {
        base.Initialize(gameObject, enemy, wall, speed, leftX, rightX, topY, bottomY);
        this.wall = wall;
        this.speed = speed;
        this.leftX = leftX;
        this.rightX = rightX;
        this.topY = topY;
        this.bottomY = bottomY;
    }

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        //Debug.Log("Idle");
        goright = false;
        if(enemy.IsFacingRight){
            enemy.transform.localScale = new Vector3(-1, 1, 1);
            goright = true;
        } else{
            enemy.transform.localScale = new Vector3(1, 1, 1);
        }
        if(wall == 1){
            enemy.RB.velocity = new Vector2(-speed*enemy.transform.localScale.x, 0);
        } else if(wall == 2){
            enemy.RB.velocity = new Vector2(0, speed*enemy.transform.localScale.x);
        } else if(wall == 3){
            enemy.RB.velocity = new Vector2(speed*enemy.transform.localScale.x, 0);
        } else if(wall == 4){
            enemy.RB.velocity = new Vector2(0, -speed*enemy.transform.localScale.x);
        }
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if(
        (wall == 1 && ((enemy.transform.position.x >= rightX && goright) || ((enemy.transform.position.x < leftX) &! goright))) ||
        (wall == 2  && ((enemy.transform.position.y >= topY &! goright) || ((enemy.transform.position.y < bottomY) && goright))) ||
        (wall == 3 && ((enemy.transform.position.x >= rightX &! goright) || ((enemy.transform.position.x < leftX) && goright))) ||
        (wall == 4 && ((enemy.transform.position.y >= topY && goright) || ((enemy.transform.position.y < bottomY) &! goright)))
        ){
            if(goright){
                enemy.transform.localScale = new Vector3(1, 1, 1);
            }else{
                enemy.transform.localScale = new Vector3(-1, 1, 1);
            }
            goright = !goright;
            if(wall == 1){
                enemy.RB.velocity = new Vector2(-speed*enemy.transform.localScale.x, 0);
            } else if(wall == 2){
                enemy.RB.velocity = new Vector2(0, speed*enemy.transform.localScale.y);
            } else if(wall == 3){
                enemy.RB.velocity = new Vector2(speed*enemy.transform.localScale.x, 0);
            } else if(wall == 4){
                enemy.RB.velocity = new Vector2(0, -speed*enemy.transform.localScale.y);
            }
        }
        
    }
    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }
}
