using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CinemachineConfiner2D>().InvalidateCache();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
