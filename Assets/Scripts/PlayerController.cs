using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;

    public float speed = 5f;

    public bool IsMoving { get; private set; }

    Rigidbody2D rb;

   private void Awake(){
        rb = GetComponent<Rigidbody2D>();
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
}
