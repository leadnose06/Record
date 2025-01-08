using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightCheckerScript : MonoBehaviour
{
    public Enemy enemy;
    public void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            enemy.CheckSight(collider);
        }
    }
}
