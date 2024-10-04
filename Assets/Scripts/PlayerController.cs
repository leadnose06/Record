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

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
    }

    public void onMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
    }

    public void onJump(InputAction.CallbackContext context){
        //TODO check if alive so no jumping during death animations
        if(context.started && touchingDirections.isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
}
