using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_LineController : MonoBehaviour
{
    private LineRenderer lr;
    private Transform[] points;

    private void Awake(){
        lr = GetComponent<LineRenderer>();
        this.points = new Transform[0];
    }

    public void SetUpLine(Transform[] points){
        lr.positionCount = points.Length;
        this.points = points;
    }

    private void Update(){
        for(int i = 0; i < points.Length; i++){
            lr.SetPosition(i, points[i].position);
        }
    }

    public void Reset(){
        lr.positionCount = 0;
        this.points = new Transform[0];
    }
  
}
