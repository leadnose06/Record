using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public bool IsMoving { get; private set; }
    public float jumpImpulse = 10f;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Vector2 moveInput;
    public Animator moveAnimator;
    public Animator attackPointAnimator;
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
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;


    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animationLock = false;
        dashReady = true;
    }



    // Update is called once per frame
    void Update()
    {
        //flips player based on horizontal movement
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
        
        //triggers movement/idle animation based on whether or not the player is moving
    
        //Debug.Log("" + rb.velocity.x);
        moveAnimator.SetBool("IsRunning", IsMoving);
        
        
        if(!isDashing && !dashReady && Time.time >= dashTimer && touchingDirections.isGrounded){
            dashReady = true;
        }
        if (idleTimer >= 20f) {moveAnimator.SetBool("Idle", true);}
        else {idleTimer += Time.deltaTime;}

        if (bladeOut) {bladeTimer += Time.deltaTime;}
        if (bladeTimer >= 5f) {
            bladeOut = false;
            bladeTimer = 0f;
            moveAnimator.SetBool("Blade Out",false);
        }
    }

    private void FixedUpdate() {
        if(!touchingDirections.isOnWall && !animationLock){
            rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
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
                Debug.Log("avg: "+all/num + " max: "+max);
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
        if(context.performed && touchingDirections.isGrounded && !animationLock){
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            moveAnimator.SetBool("Idle",false);
            idleTimer = 0f;
            moveAnimator.SetTrigger("PlayerJump");
            Debug.Log("jump trigger");
        }
        
        
    }
    public void onDash(InputAction.CallbackContext context){
        if(context.performed && dashReady && !animationLock){
            dashSign = transform.localScale.x;
            dashTimer = Time.fixedTime + 0.135f;
            dashReady = false;
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
            DataManager.Instance.lastBench = SceneManager.GetActiveScene().name;
            Debug.Log("new bench");
        }

    }

    //Attacking Methods

    public void onAttack(InputAction.CallbackContext context){
        if(context.performed){
            Debug.Log("Attack");
            //Play attack animation
            moveAnimator.SetBool("Idle",false);
            moveAnimator.SetTrigger("Attack");
            attackPointAnimator.SetTrigger("Attack");
            idleTimer = 0;
            bladeOut = true;
            moveAnimator.SetBool("Blade Out",true);
            //Detect enemies in range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            //Damage them
            foreach(Collider2D enemy in hitEnemies) {
                Debug.Log("Hit");
                enemy.GetComponent<Enemy>().Damage(5.0f);
            }
        }
    }

    void OnDrawGizmosSelected(){
        if (attackPoint == null) {return;}
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }




    public void damage(int amount){
        GetComponent<Health>().health -= 1;
        DataManager.Instance.playerHealth -= 1;
        if(DataManager.Instance.playerHealth < 1){
            death();
        }
        moveAnimator.SetBool("Idle",false);
        idleTimer = 0f;
    }



    public void death(){
        DataManager.Instance.dead = true;
        if(SceneManager.GetActiveScene().name.Equals(DataManager.Instance.lastBench)){
            DataManager.Instance.bench.GetComponent<BenchScript>().respawn();
        }else{
            SceneManager.LoadScene(DataManager.Instance.lastBench);
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag.Equals("OutOfBounds")){
            transform.position = gameObject.GetComponent<RespawnScript>().getRespawn.position;
            damage(1);
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
}
