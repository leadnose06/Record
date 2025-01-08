using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class GrapplingScript : MonoBehaviour
{
    public GameObject objectTracker;
    private GameObject[] grappleables;
    private List<Transform> LegalTargets;
    private List<float> LegalAngleDiffs;
    public bool IsGrappling = false;
    public GameObject grappleHookPattern;
    public GameObject grappleHook;
    private float grappleCooldown;
    public GameObject attached;
    Rigidbody2D rb;
    private float minMax = 20f;
    public float grappleSpeed;
    private float distToTarget = 100f;
    public lr_LineController line;
    public ContactFilter2D contactFilter;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake(){
        grappleables = objectTracker.GetComponent<ObjectTrackerScript>().enemies.Concat(objectTracker.GetComponent<ObjectTrackerScript>().grapples).ToArray();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(IsGrappling){
            if(distToTarget <= 1.005f){
                onDisconnect();
            }
        }

    }
    public void onConnect(){
        line.GetComponent<lr_LineController>().SetUpLine(new Transform[2]{transform, attached.transform});
        IsGrappling = true;
        gameObject.GetComponent<PlayerController>().animationLock = true;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, 0);
    }
    //todo: refresh dash and double jump
    public void onDisconnect(){
        Destroy(grappleHook);
        line.GetComponent<lr_LineController>().Reset();
        IsGrappling = false;
        gameObject.GetComponent<PlayerController>().animationLock = false;
        rb.gravityScale = 1;
        rb.velocity = new Vector2(0, 0);
        distToTarget = 100f;
        grappleCooldown = Time.time + 1f;
    }

    //todo: update for elites and bosses
    private void FixedUpdate(){
        if(IsGrappling){
            RaycastHit2D grappleCheck = Physics2D.Raycast(transform.position, attached.transform.position - transform.position, Vector2.Distance(new Vector2(attached.transform.position.x, attached.transform.position.y), new Vector2(transform.position.x, transform.position.y)));
            if(grappleCheck.collider.tag == "Wall"){
                onDisconnect();
            } else{
                Vector2 direction = attached.transform.position - transform.position;
                rb.velocity = direction.normalized * grappleSpeed;
                distToTarget = Vector2.Distance(new Vector2(attached.transform.position.x, attached.transform.position.y), new Vector2(transform.position.x, transform.position.y));
            }
        }
    }

    public void onFire(InputAction.CallbackContext context){
        if(Time.time >= grappleCooldown && !IsGrappling && context.performed == true && !gameObject.GetComponent<PlayerController>().animationLock){
            var gamepad = Gamepad.current;
            Vector2 move = gamepad.rightStick.ReadValue();
            grappleables = objectTracker.GetComponent<ObjectTrackerScript>().enemies.Concat(objectTracker.GetComponent<ObjectTrackerScript>().grapples).ToArray();
            LegalTargets = new List<Transform>();
            LegalAngleDiffs = new List<float>();
            if(Mathf.Abs(move.x) > 0.001f || Mathf.Abs(move.y) > 0.001f){
                float JoystickAngle = (Mathf.Rad2Deg*Mathf.Atan2(move.y, move.x)+360)%360;
                float min = (JoystickAngle - minMax + 360)%360;
                float max = (JoystickAngle + minMax)%360;
                foreach(GameObject a in grappleables){
                    RaycastHit2D grappleCheck = Physics2D.Raycast(transform.position, a.transform.position - transform.position, 100f);
                    float TargetAngle = (Mathf.Rad2Deg * Mathf.Atan2(grappleCheck.transform.position.y-transform.position.y, grappleCheck.transform.position.x-transform.position.x)+360)%360;
                    if(grappleCheck.collider.tag == "Grappleable" || grappleCheck.collider.tag == "Enemy" ){
                        if(max + 180 < min) {
                            if(TargetAngle > min || TargetAngle < max){
                                LegalTargets.Add(grappleCheck.transform);
                                LegalAngleDiffs.Add(Mathf.Abs(((JoystickAngle+180)%360)-((TargetAngle+180)%360)));
                            }
                        }
                        else{
                            if(TargetAngle > min && TargetAngle < max){
                                LegalTargets.Add(grappleCheck.transform);
                                LegalAngleDiffs.Add(Mathf.Abs(JoystickAngle-TargetAngle));
                            }
                        }
                    }
                }                                
                grappleHook = Instantiate(grappleHookPattern);
                grappleHook.transform.position = new Vector3(transform.position.x, transform.position.y);
                grappleHook.GetComponent<GrappleHookScript>().player = gameObject;
                if(LegalTargets.Count > 0 && LegalAngleDiffs.Count > 0){
                    grappleHook.GetComponent<GrappleHookScript>().targetPos = LegalTargets[LegalAngleDiffs.IndexOf(LegalAngleDiffs.Min())].position;
                    grappleHook.GetComponent<GrappleHookScript>().target = true;
                }else{
                    grappleHook.GetComponent<GrappleHookScript>().targetDir = JoystickAngle;
                    grappleHook.GetComponent<GrappleHookScript>().target = false;
                }
                grappleHook.GetComponent<GrappleHookScript>().line = this.line;
                grappleHook.SetActive(true);
                line.GetComponent<lr_LineController>().SetUpLine(new Transform[2]{transform, grappleHook.transform});
                grappleCooldown = Time.time;
            }
        }
    }
    public void onRelease(InputAction.CallbackContext context){
        if(IsGrappling){
            if(context.canceled == true){
                onDisconnect();
            }
        }
    }
}
