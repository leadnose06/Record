using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrapplingScript : MonoBehaviour
{
    public GameObject objectTracker;
    private GameObject[] grappleables;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake(){
        grappleables = objectTracker.GetComponent<ObjectTrackerScript>().enemies.Concat(objectTracker.GetComponent<ObjectTrackerScript>().grapples).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
