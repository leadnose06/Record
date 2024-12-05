using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IEnemyMovable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    public Rigidbody2D RB { get; set;}
    [field: SerializeField] public bool IsFacingRight { get; set;}

    public EnemyStateMachine StateMachine {get; set;}
    public PatrolState patrolState {get; set;}
    public ChaseState chaseState {get; set;}

    #region PatrolVariables
        public Transform LeftPoint;
        public Transform RightPoint;
        public float VariancePercent = 15;
        public float speed = 5;

    #endregion
    
    void Awake(){
        patrolState = new PatrolState(this, StateMachine);
        chaseState = new ChaseState(this, StateMachine);
    }
    void Start()
    {
        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(patrolState);
    }

    void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    void FixedUpdate(){
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        if(CurrentHealth <= 0f){
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void MoveEnemy(Vector2 velocity)
    {
        RB.velocity = velocity;
        CheckFacing(velocity);
    }

    public void CheckFacing(Vector2 velocity){
        if (IsFacingRight && velocity.x < 0f){
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        } else if (!IsFacingRight && velocity.x < 0f){
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void AnimationTriggerEvent( AnimationTriggerType triggerType){
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType{
        EnemyDamaged,
        EnemyDied
    }


}
