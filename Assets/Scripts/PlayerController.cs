using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public bool IsMoving { get; private set; }
    public float jumpImpulse = 10f;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Vector2 moveInput;
    private Animator moveAnimator;
    public GameObject player;
    private bool firstFrameOfInput;


    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        moveAnimator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //flips player based on horizontal movement
        if (Input.GetAxis("Horizontal") > 0.01f) {transform.localScale = Vector3.one;}
        else if (Input.GetAxis("Horizontal") < -0.01f) {transform.localScale = new Vector3(-1,1,1);}
        
        //triggers movement/idle animation based on whether or not the player is moving
    
       //Debug.Log("" + rb.velocity.x);
       moveAnimator.SetBool("IsRunning", IsMoving);
    }

    private void FixedUpdate() {
        if(!touchingDirections.isOnWall && !GetComponent<GrapplingScript>().IsGrappling){
            rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
        }
    }

    public void onMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();

        //IsMoving = moveInput != Vector2.zero;
        IsMoving = moveInput.x != 0f;
      
        
        
    }

    public void onJump(InputAction.CallbackContext context){
        //TODO check if alive so no jumping during death animations
        if(context.started && touchingDirections.isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
}
