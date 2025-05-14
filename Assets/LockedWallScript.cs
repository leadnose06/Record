using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedWallScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lockedWall;
    public int minibossNumber;
    public bool minibossDead;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (minibossNumber == 1 && DataManager.Instance.miniboss1Dead) {
            minibossDead = true;
        }
        else if (minibossNumber == 2 && DataManager.Instance.miniboss2Dead) {
            minibossDead = true;
        }
        else if (minibossNumber == 3 && DataManager.Instance.miniboss3Dead) {
            minibossDead = true;
        }

        if (minibossDead) {
            Destroy(lockedWall);
        }
    }
}
