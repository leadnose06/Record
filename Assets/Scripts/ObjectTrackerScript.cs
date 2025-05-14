using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectTrackerScript : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] grapples;
    public GameObject[] elites;
    public GameObject[] boss;
    public bool bossDead;
    public string level;


    // Start is called before the first frame update
    void Start()
    {
        bossDead = false;
    }


    // Update is called once per frame
    void Update()
    {
        bossDead = true;
        for (int x = 0; x < boss.Length; x++) {
            if (boss[x].gameObject.activeSelf) {
                bossDead = false;
                Debug.Log("Boss not dead");
            }
        }
        if (bossDead){
            Debug.Log("A door has opened");
            if (level.Equals("B3")){
                DataManager.Instance.miniboss1Dead = true;
            }
            else if (level.Equals("B5")){
                DataManager.Instance.miniboss2Dead = true;
            }
            else if (level.Equals("E5")){
                DataManager.Instance.miniboss3Dead = true;
            }
        }
    }
}
