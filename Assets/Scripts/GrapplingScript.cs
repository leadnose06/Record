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
    public bool IsGrappling = false;

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
        if(!IsGrappling){
            var gamepad = Gamepad.current;
            Vector2 move = gamepad.rightStick.ReadValue();
            grappleables = objectTracker.GetComponent<ObjectTrackerScript>().enemies.Concat(objectTracker.GetComponent<ObjectTrackerScript>().grapples).ToArray();
            LegalTargets = new Transform[0];
            if(Mathf.Abs(move.x) > 0.001 || Mathf.Abs(move.y) > 0.001){
                float JoystickAngle = Mathf.Rad2Deg*Mathf.Atan(move.y/move.x);
                float min = (JoystickAngle - minMax + 360)%360;
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
                            }
                        }
                        else{
                            if(TargetAngle > min && TargetAngle < max){
                                LegalTargets.Append(grappleCheck.transform);
                            }
                        }
                    }
                }
            }
        }

    }
}
