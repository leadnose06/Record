using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingScript : MonoBehaviour
{
    public GameObject objectTracker;
    private GameObject[] grappleables;
    private Transform[] LegalTargets;
    private float[] LegalAngleDiffs;
    public bool IsGrappling = false;
    public GameObject grappleHookPattern;
    public GameObject grappleHook;

    [SerializeField]private float minMax = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake(){
        grappleables = objectTracker.GetComponent<ObjectTrackerScript>().enemies.Concat(objectTracker.GetComponent<ObjectTrackerScript>().grapples).ToArray();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

    }
    public void onFire(InputAction.CallbackContext context){
        if(!IsGrappling){
            var gamepad = Gamepad.current;
            Vector2 move = gamepad.rightStick.ReadValue();
            grappleables = objectTracker.GetComponent<ObjectTrackerScript>().enemies.Concat(objectTracker.GetComponent<ObjectTrackerScript>().grapples).ToArray();
            LegalTargets = new Transform[0];
            LegalAngleDiffs = new float[0];
            if(Mathf.Abs(move.x) > 0.001f || Mathf.Abs(move.y) > 0.001f){
                float JoystickAngle = Mathf.Rad2Deg*Mathf.Atan(move.y/move.x);
                /*float min = (JoystickAngle - minMax + 360)%360;
                float max = (JoystickAngle + minMax)%360;
                foreach(GameObject a in grappleables){
                    RaycastHit2D grappleCheck = Physics2D.Raycast(transform.position, a.transform.position - transform.position, 100f);
                    float TargetAngle = Mathf.Rad2Deg * Mathf.Atan(grappleCheck.transform.position.y/grappleCheck.transform.position.x);
                    Debug.Log(grappleCheck.distance);
                    Debug.DrawRay(transform.position, a.transform.position);
                    if(grappleCheck.collider.tag == "Grappleable" || grappleCheck.collider.tag == "Enemy" ){
                        if(max + 180 < min) {
                            if(TargetAngle > min || TargetAngle < max){
                                LegalTargets.Append(grappleCheck.transform);
                                LegalAngleDiffs.Append(Mathf.Abs(((JoystickAngle+180)%360)-((TargetAngle+180)%360)));
                            }
                        }
                        else{
                            if(TargetAngle > min && TargetAngle < max){
                                LegalTargets.Append(grappleCheck.transform);
                                LegalAngleDiffs.Append(Mathf.Abs(JoystickAngle-TargetAngle));
                            }
                        }
                    }
                }*/
                        
                grappleHook = Instantiate(grappleHookPattern);
                grappleHook.transform.position = transform.position;
                grappleHook.GetComponent<GrappleHookScript>().player = gameObject;
                //grappleHook.GetComponent<GrappleHookScript>().targetPos = LegalTargets[Array.IndexOf(LegalAngleDiffs, LegalAngleDiffs.Min())].position;
                grappleHook.GetComponent<GrappleHookScript>().targetDir = JoystickAngle;
                grappleHook.SetActive(true);
                IsGrappling = true;
            }


        }
    }
}
