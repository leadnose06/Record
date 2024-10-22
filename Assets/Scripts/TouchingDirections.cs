using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    CapsuleCollider2D touchingCol;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    public float groundDistance = 0.05f;
    [SerializeField]
    private bool _isGrounded;
    private bool _isOnWall;
    private bool _isOnCeiling;
    private Vector2 wallCheckDirection;

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
    
    

    private void Awake(){
        touchingCol = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        isGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        //isOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        //isOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }


    
}
