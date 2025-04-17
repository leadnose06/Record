using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public GameObject dataManager;
    public float speed = 5f;
    public bool IsMoving { get; private set; }
    public float jumpImpulse = 10f;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Vector2 moveInput;
    public Animator moveAnimator;
    
    private bool firstFrameOfInput;
    public bool animationLock;
    public float dashTimer = 0f;
    public bool dashReady;
    private float dashSign;
    public bool isDashing;
    private float dashDist;

    private float max=100;
    private float all=0;
    private int num = 0;
    private bool inBench = false;
    private float idleTimer = 20f;
    
    //Attack Variables
    private float bladeTimer;
    private bool bladeOut;
    public Transform frontAttackPoint;
    public Transform upAttackPoint;
    public Transform downAttackPoint;
    public Animator frontAttackPointAnimator;
    public Animator upAttackPointAnimator;
    public Animator downAttackPointAnimator;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public bool downHit; 
    public float attackTimer = 1.0f;
    public float attackDelay = 0.5f;
    public int downHitBounceAmount = 3;

    // Heal Variables
    public int healAmount = 2;
    //Variables for getting hit (color change + knockback)
    public SpriteRenderer sprite;
    public bool colorIsRed;
    public float colorHitTimer;
    private Vector2 knockbackVelocity;
    public bool experiencingKnockback = false;
    private float knockbackTimer;
    
    //generic timer for debugging
    private float timer;
    
    
    
    
   



    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animationLock = false;
        dashReady = true;
        Physics2D.IgnoreLayerCollision(3,6);
    }



    // Update is called once per frame
    void Update()
    {
        if(CanvasScript.paused == true) return;
        //flips player based on horizontal movement, does not work wheb attacking
        if (attackTimer > 0.45f) {
            if (Input.GetAxis("Horizontal") > 0.01f) {
                moveAnimator.SetBool("Idle",false);
                idleTimer = 0f;
                transform.localScale = Vector3.one;
            }
            else if (Input.GetAxis("Horizontal") < -0.01f) {
                transform.localScale = new Vector3(-1,1,1);
                moveAnimator.SetBool("Idle",false);
                idleTimer = 0f;
            }
        }
       
    
        //Debug.Log("" + rb.velocity.x);
        moveAnimator.SetBool("IsRunning", IsMoving);
        
        //Dashing
        dashReady = DataManager.Instance.dashReady;
        if(!isDashing && !dashReady && Time.time >= dashTimer && touchingDirections.isGrounded){
            dashReady = true;
            DataManager.Instance.dashReady = true;
        }
        if (idleTimer >= 20f) {moveAnimator.SetBool("Idle", true);}
        else {idleTimer += Time.deltaTime;}

        if (bladeOut) {bladeTimer += Time.deltaTime;}
        if (bladeTimer >= 5f) {
            bladeOut = false;
            bladeTimer = 0f;
            moveAnimator.SetBool("Blade Out",false);
        }

        //attacking timer
        if (attackTimer < attackDelay) {
            attackTimer += Time.deltaTime;
            //Debug.Log(attackTimer);
        }
        //hit timer
        if (colorIsRed && colorHitTimer <= 0) {
            sprite.color = Color.cyan;
            colorIsRed = false;
        }
        else {colorHitTimer -= Time.deltaTime;}
       
       /*if (timer <= 0f){
        timer = 0.25f;
        Debug.Log("velocity x: " + rb.velocity.x);
       }
       else {
        timer -= Time.deltaTime;
       }*/

        if (knockbackTimer > 0){
            knockbackTimer -= Time.deltaTime;
        }
        else {
            experiencingKnockback = false;
        }
    }

    private void FixedUpdate() {
        if(!touchingDirections.isOnWall && !animationLock && !experiencingKnockback){
            rb.velocity = new Vector2(moveInput.normalized.x * speed, rb.velocity.y);
        }
        if(isDashing){
            rb.MovePosition(new Vector2(transform.position.x + (dashSign * 8f * Time.fixedDeltaTime), transform.position.y));
        }
        if(isDashing){
            if(Time.fixedTime >= dashTimer){
                animationLock = false;
                isDashing = false;
                rb.gravityScale = 1;
                dashTimer = Time.time + 0.5f;
                num++;
                all+=Mathf.Abs(transform.position.x - dashDist);
                if(Mathf.Abs(transform.position.x - dashDist)<max){max = Mathf.Abs(transform.position.x - dashDist);}
                //Debug.Log("avg: "+all/num + " max: "+max);
            }
        }
    }

    
    
    public void onMove(InputAction.CallbackContext context) {
        moveAnimator.SetBool("Idle",false);
        idleTimer = 0f;
        moveInput = context.ReadValue<Vector2>();

        //IsMoving = moveInput != Vector2.zero;
        IsMoving = moveInput.x != 0f;
      
        
        
    }

    public void onJump(InputAction.CallbackContext context){
        //TODO check if alive so no jumping during death animations
        if(context.performed && (touchingDirections.isGrounded || DataManager.Instance.doubleJumpReady) && !animationLock){
            if (!touchingDirections.isGrounded) {DataManager.Instance.doubleJumpReady = false;}
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            moveAnimator.SetBool("Idle",false);
            idleTimer = 0f;
            moveAnimator.SetTrigger("PlayerJump");
            //Debug.Log("jump trigger");
        }
        
        
    }
    public void onDash(InputAction.CallbackContext context){
        if(context.performed && dashReady && !animationLock){
            dashSign = transform.localScale.x;
            dashTimer = Time.fixedTime + 0.135f;
            dashReady = false;
            DataManager.Instance.dashReady = false;
            animationLock = true;
            isDashing = true;
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0;
            dashDist = transform.position.x;
        }
        moveAnimator.SetBool("Idle",false);
        idleTimer = 0f;
    }

    public void onInteract(InputAction.CallbackContext context){
        if(inBench){
            GetComponent<Health>().health = DataManager.Instance.playerMaxHealth;
            DataManager.Instance.playerHealth = DataManager.Instance.playerMaxHealth;
            DataManager.Instance.playerData.lastBench = SceneManager.GetActiveScene().name;
            DataManager.Instance.SaveGame();
            Debug.Log("new bench");
        }

    }

    //Attacking Methods

    public void onAttack(InputAction.CallbackContext context){
        if(context.performed){
            if (attackTimer >= attackDelay) {
                //Debug.Log("Attack");
                //Play attack animation
                moveAnimator.SetBool("Idle",false);
                moveAnimator.SetTrigger("Attack");
                idleTimer = 0;
                bladeOut = true;
                moveAnimator.SetBool("Blade Out",true);
            
                //Detect enemies in range of attack, choosing certain attack point based on direction of input
                bool hit = false;
                Collider2D[] hitEnemies;
                if (Input.GetAxis("Vertical") > 0.5f){
                    hitEnemies = Physics2D.OverlapCircleAll(upAttackPoint.position, attackRange, enemyLayers);
                    upAttackPointAnimator.SetTrigger("Attack");
                }
                else if (Input.GetAxis("Vertical") < -0.5f && !GetComponent<TouchingDirections>().isGrounded){
                    hitEnemies = Physics2D.OverlapCircleAll(downAttackPoint.position, attackRange, enemyLayers);
                    downAttackPointAnimator.SetTrigger("Attack");
                    downHit = true;
                }
                else {
                    hitEnemies = Physics2D.OverlapCircleAll(frontAttackPoint.position, attackRange, enemyLayers);
                    frontAttackPointAnimator.SetTrigger("Attack");
                    }
                
                //Damage them
                foreach(Collider2D enemy in hitEnemies) {
                    //Debug.Log("Hit");
                    hit = true;
                    enemy.GetComponent<Enemy>().Damage(5.0f);
                }
                if (hit) {
                    DataManager.Instance.playerEnergy += 1;
                    //Debug.Log("energy level up");
                }
                else {downHit = false;}
                attackTimer =0f;
                //propel player upwards if hitting enemy below
                if (downHit) {
                    rb.velocity = new Vector2(rb.velocity.x, downHitBounceAmount);
                    downHit = false;
                    dashReady = true;
                    DataManager.Instance.doubleJumpReady = true;
                }
            }

            
        }
    }

    public void onHeal(InputAction.CallbackContext context){
        if (context.performed && DataManager.Instance.playerHeals > 0 && DataManager.Instance.playerHealth != DataManager.Instance.playerMaxHealth) {
            DataManager.Instance.playerHealth += healAmount;
            //Debug.Log("healed " + healAmount);
            if (DataManager.Instance.playerHealth > DataManager.Instance.playerMaxHealth) {
                DataManager.Instance.playerHealth = DataManager.Instance.playerMaxHealth;
            }
            DataManager.Instance.playerHeals -= 1;
            Debug.Log(DataManager.Instance.playerHeals + " heals left");
            
        }
        
    }



    void OnDrawGizmosSelected(){
        if (frontAttackPoint == null) {return;}
        Gizmos.DrawWireSphere(frontAttackPoint.position, attackRange);
    }




    public void damage(int amount){
        if (DataManager.Instance.playerHealth > 0){
            GetComponent<Health>().health -= 1;
            DataManager.Instance.playerHealth -= 1;
        }
        if(DataManager.Instance.playerHealth < 1){
            death();
        }
        moveAnimator.SetBool("Idle",false);
        idleTimer = 0f;
        colorIsRed = true;
        sprite.color = Color.red;
        colorHitTimer = 0.1f;
    }



    public void death(){
        DataManager.Instance.dead = true;
        if(SceneManager.GetActiveScene().name.Equals(DataManager.Instance.playerData.lastBench)){
            DataManager.Instance.bench.GetComponent<BenchScript>().respawn();
        }else{
            SceneManager.LoadScene(DataManager.Instance.playerData.lastBench);
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag.Equals("OutOfBounds")){
            transform.position = new Vector3(gameObject.GetComponent<RespawnScript>().getRespawn.position.x, gameObject.GetComponent<RespawnScript>().getRespawn.position.y, transform.position.z);
            damage(1);
            //Debug.Log("Hit by enemy");
            GetComponent<GrapplingScript>().onDisconnect();
        }
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D col){
        if(col.tag.Equals("Bench")){
            inBench = true;
        }
    }
        void OnTriggerExit2D(UnityEngine.Collider2D col){
        if(col.tag.Equals("Bench")){
            inBench = false;
        }
    }

    public void Knockback(bool right,int damageAmount,Collider2D collision){
        damage(damageAmount);
        Vector3 direction = transform.position - collision.gameObject.transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * 6; //-Mathf.Sqrt(damageAmount*10);
        //Debug.Log("knocked back");
        //Debug.Log("Vector Coordinates: " + direction.x + " " + direction.y);
        //Debug.Log("Velocity: " + rb.velocity.x);
        knockbackTimer = 0.25f;
        experiencingKnockback = true;
    }
}
