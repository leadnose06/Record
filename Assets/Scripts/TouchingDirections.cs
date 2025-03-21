using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    CapsuleCollider2D touchingCol;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    public bool jumped;
    public bool initialFall = true;

    [SerializeField]
    private bool _isGrounded;
    private bool _isOnWall;
    private bool _isOnCeiling;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool isGrounded { get{
        return _isGrounded;
    } private set{
        _isGrounded = value;
    } }

    public bool isOnWall { get{
        return _isOnWall;
    } private set{
        _isOnWall = value;

    } }public bool isOnCeiling { get{
        return _isOnCeiling;
    } private set{
        _isOnCeiling = value;
    } }
    
    private Animator moveAnimator;
    

    private void Awake(){
        touchingCol = GetComponent<CapsuleCollider2D>();
        moveAnimator = GetComponent<Animator>();
    }

    public void jump(){
        jumped = true;
    }

    void FixedUpdate()
    {
        
        isGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        moveAnimator.SetBool("IsGrounded",isGrounded);
        if (isGrounded) {
            if (moveAnimator.GetBool("IsFalling")) {
                //Debug.Log("not falling");
            }
            moveAnimator.SetBool("IsFalling", false);
            initialFall = true;
            jumped = false;
            //double jump refresh
            DataManager.Instance.doubleJumpReady = true;
        } else {
            if (!moveAnimator.GetBool("IsFalling")) {
                //Debug.Log("falling");
            }
            if (jumped) {
                moveAnimator.SetBool("IsFalling", true);
                jumped = false;
            }
            else if (initialFall){
                moveAnimator.SetTrigger("FallingNoJump");
                initialFall = false;
            }
            
            
        }
        isOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        isOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
        
    }


    
}
