using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedWallTriggerScript : MonoBehaviour
{
    public GameObject[] lockedWalls;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject wall in lockedWalls)
        {
            wall.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject wall in lockedWalls)
        {
            wall.SetActive(true);
        }
    }
}
