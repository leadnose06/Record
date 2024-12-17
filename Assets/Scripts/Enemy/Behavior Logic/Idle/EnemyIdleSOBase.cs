using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyIdleSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;
    protected Transform playerTransform;

    public virtual void Initialize(GameObject gameObject, Enemy enemy){
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    #region specific initialize functions
    //patrol
    public virtual void Initialize(GameObject gameObject, Enemy enemy, Transform LeftPoint, Transform RightPoint, float speed){
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    //loop: 1 is top wall, clockwise from there 
    public virtual void Initialize(GameObject gameObject, Enemy enemy, int wall, Collider2D FloorCheck, Collider2D WallCheck){
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    #endregion

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() {
        if(enemy.IsAggroed){
            enemy.StateMachine.ChangeState(enemy.EnemyChaseState);
        }
     }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
