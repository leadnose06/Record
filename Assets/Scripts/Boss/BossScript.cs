using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] private GameObject laserPattern;
    [SerializeField] private GameObject laserPreviewPattern;
    [SerializeField] private GameObject bulletPattern;
    public GameObject[] laserArray;
    public GameObject[] laserPreviewArray;
    public GameObject[] bulletArray;
    void Start()
    {
        laserArray = new GameObject[3];
        laserPreviewArray = new GameObject[3];
        for(int i=0; i<3; i++){
            laserArray[i] = Instantiate(laserPattern);
            laserPreviewArray[i] = Instantiate(laserPreviewPattern);
        }
    }

    void Update()
    {
        
    }
}
