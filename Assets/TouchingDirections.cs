using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    BoxCollider2D touchingCol;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    public float groundDistance = 0.05f;
    [SerializeField]
    private bool _isGrounded;

    public bool isGrounded { get{
        return _isGrounded;
    } private set{
        _isGrounded = value;
    } }
    
    

    private void Awake(){
        touchingCol = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        isGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }


    
}
