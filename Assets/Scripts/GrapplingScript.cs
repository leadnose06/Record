using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrapplingScript : MonoBehaviour
{
    public GameObject objectTracker;
    private GameObject[] grappleables;
    private Transform[] LegalTargets;
    public bool IsGrappling = false;
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
            grappleables = objectTracker.GetComponent<ObjectTrackerScript>().enemies.Concat(objectTracker.GetComponent<ObjectTrackerScript>().grapples).ToArray();
            foreach(GameObject a in grappleables){
                RaycastHit2D grappleCheck = Physics2D.Raycast(transform.position, a.transform.position - transform.position, 100f);
                Debug.Log(grappleCheck.distance);
                Debug.DrawRay(transform.position, a.transform.position, Color.black, 10f);
            }

        }

    }
}
