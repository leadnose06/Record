using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Loop Around", menuName = "Enemy Logic/Idle Logic/Loop Around")]
public class LoopAround : EnemyIdleSOBase
{
    private int wall;
    private bool right;
    private RaycastHit2D[] FloorHits = new RaycastHit2D[5];
    private RaycastHit2D[] WallHits = new RaycastHit2D[5];
    private bool turn = false;
    private bool turnDown = false;
    Collider2D FloorCheck;
    Collider2D WallCheck;
    //[SerializeField] private ContactFilter2D castFilter;
    public override void Initialize(GameObject gameObject, Enemy enemy, int wall, Collider2D FloorCheck, Collider2D WallCheck){
        base.Initialize(gameObject, enemy, wall, FloorCheck, WallCheck);
        this.wall = wall;
        this.FloorCheck = FloorCheck;
        this.WallCheck = WallCheck;
    }

    public override void DoEnterLogic(){
        base.DoEnterLogic();
        right = enemy.IsFacingRight;
        enemy.RB.velocity = new Vector2(1, -0.1f);

    }

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if((wall == 1 && right ) || (wall == 3 &! right)){
            if(FloorCheck.Cast(Vector2.right, FloorHits, 0.15f)==0){
                enemy.transform.Rotate(new Vector3(0, 0, 90));
                if(wall == 1){
                    wall = 2;
                    enemy.RB.velocity = new Vector2(-0.1f, 1);
                }
            }
        }

    }
    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }
    public override void ResetValues()
    {
        base.ResetValues();
    }
}
