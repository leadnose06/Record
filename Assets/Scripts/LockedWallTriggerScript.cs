using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedWallTriggerScript : MonoBehaviour
{
    public GameObject boss;
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
        if (collision.gameObject.tag == "Player") {
            foreach (GameObject wall in lockedWalls)
            {
                wall.SetActive(true);
            }
            if (boss)
            {
                boss.GetComponent<BossScript>().Activate();
            }
        }
    }
}
