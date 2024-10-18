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

    private float minMax = 20f;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake(){
        grappleables = objectTracker.GetComponent<ObjectTrackerScript>().enemies.Concat(objectTracker.GetComponent<ObjectTrackerScript>().grapples).ToArray();
    }

    void Update()
    {
        if(IsGrappling){
            if(Time.time >= grappleCooldown+1.5f){
                IsGrappling = false;
            }
        }

    }
    public void onFire(InputAction.CallbackContext context){
        if(!IsGrappling){
            var gamepad = Gamepad.current;
            Vector2 move = gamepad.rightStick.ReadValue();
            grappleables = objectTracker.GetComponent<ObjectTrackerScript>().enemies.Concat(objectTracker.GetComponent<ObjectTrackerScript>().grapples).ToArray();
            LegalTargets = new List<Transform>();
            LegalAngleDiffs = new List<float>();
            if(Mathf.Abs(move.x) > 0.001f || Mathf.Abs(move.y) > 0.001f){
                float JoystickAngle = (Mathf.Rad2Deg*Mathf.Atan2(move.y, move.x)+360)%360;
                //
                float min = (JoystickAngle - minMax + 360)%360;
                float max = (JoystickAngle + minMax)%360;
                foreach(GameObject a in grappleables){
                    RaycastHit2D grappleCheck = Physics2D.Raycast(transform.position, a.transform.position - transform.position, 100f);
                    float TargetAngle = (Mathf.Rad2Deg * Mathf.Atan2(grappleCheck.transform.position.y, grappleCheck.transform.position.x)+360)%360;
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
                }//
                        
                grappleHook = Instantiate(grappleHookPattern);
                grappleHook.transform.position = new Vector3(transform.position.x, transform.position.y);
                grappleHook.GetComponent<GrappleHookScript>().player = gameObject;
                Debug.Log("Angle: "+ JoystickAngle);
                Debug.Log("min: "+ min);
                Debug.Log("max: "+ max);
                //Debug.Log(LegalTargets[0].position.x);
                if(LegalTargets.Count > 0 && LegalAngleDiffs.Count > 0){
                    grappleHook.GetComponent<GrappleHookScript>().targetPos = LegalTargets[LegalAngleDiffs.IndexOf(LegalAngleDiffs.Min())].position;
                    grappleHook.GetComponent<GrappleHookScript>().target = true;
                }else{
                    grappleHook.GetComponent<GrappleHookScript>().targetDir = JoystickAngle;
                    grappleHook.GetComponent<GrappleHookScript>().target = false;
                }
                grappleHook.SetActive(true);
                IsGrappling = true;
                grappleCooldown = Time.time;
            }


        }
    }
}
