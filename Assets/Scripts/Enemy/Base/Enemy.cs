using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IEnemyMovable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    public Rigidbody2D RB { get; set; }
    [field: SerializeField] public bool IsFacingRight { get; set; } = false;

    public EnemyStateMachine StateMachine {get; set;}
    public IdleState EnemyIdleState {get; set;}
    public ChaseState EnemyChaseState {get; set;}
    public AttackState EnemyAttackState {get; set; }
    public bool IsAggroed { get; set; }
    public bool IswithinStrikingDistance { get; set; }
    public Vector2 origin;
    public float maxDist;
    public float speed;
    public ContactFilter2D contactFilter;
    public GameObject SightChecker;


    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance {get; set;}
    public EnemyChaseSOBase EnemyChaseBaseInstance {get; set;}
    public EnemyAttackSOBase EnemyAttackBaseInstance {get; set;}
    
    protected virtual void Awake(){
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);
        StateMachine =  new EnemyStateMachine();
        EnemyIdleState = new IdleState(this, StateMachine);
        EnemyChaseState = new ChaseState(this, StateMachine);
        EnemyAttackState = new AttackState(this, StateMachine);
    }
    public virtual void Start()
    {
        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(EnemyIdleState);
    }

    public void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    public void FixedUpdate(){
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

    public virtual void CheckFacing(Vector2 velocity){
        if (IsFacingRight && velocity.x < 0f){
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
            IsFacingRight = false;
        } else if (!IsFacingRight && velocity.x > 0f){
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
            IsFacingRight = true;
        }
    }

    public virtual void CheckSight(Collider2D collider){
        RaycastHit2D results = Physics2D.Raycast(transform.position, collider.gameObject.transform.position - transform.position, Mathf.Infinity, contactFilter.layerMask);
            if(results.collider.tag == "Player"){
                SetAggroStatus(true);
                StateMachine.ChangeState(EnemyChaseState);
            }
    }

    private void AnimationTriggerEvent( AnimationTriggerType triggerType){
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetStrikingDistanceBool(bool iswithinStrikingDistance)
    {
        IswithinStrikingDistance = iswithinStrikingDistance;
    }

    public enum AnimationTriggerType{
        EnemyDamaged,
        EnemyDied
    }


}
