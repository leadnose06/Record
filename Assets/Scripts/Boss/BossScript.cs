using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] private GameObject laserPattern;
    [SerializeField] private GameObject bulletPattern;
    public GameObject[] laserArray;
    public GameObject[] bulletArray;
    public SpriteRenderer sprite;
    private bool colorIsRed;
    private float colorHitTimer;
    public float CurrentHealth { get; private set; }
    public int phase;
    private float attackCooldown;
    public bool activated;
    public int attackChoice;
    public GameObject player;

    void Start()
    {
        phase = 1;
        activated = true;
        laserArray = new GameObject[6];
        bulletArray = new GameObject[20];
        for(int i=0; i<6; i++){
            laserArray[i] = Instantiate(laserPattern);
            laserArray[i].SetActive(false);
        }
        for(int i=0; i<20; i++){
            bulletArray[i] = Instantiate(bulletPattern);
            bulletArray[i].SetActive(false);
        }
        CurrentHealth = 90f;
    }

    void Update()
    {
        if (colorIsRed && colorHitTimer <= 0) {sprite.color = Color.white;}
        else {colorHitTimer -= Time.deltaTime;}
        attackCooldown -= Time.deltaTime;
        if(activated){
            if(attackCooldown <= 0){
                if(phase == 1){
                    attackChoice = Random.Range(0, 4);
                    for(int i=0; i<laserArray.Length; i++){
                        if(laserArray[i].activeSelf == false){
                            laserTarget(i);  
                        }
                    }
                    attackCooldown = 4f;
                }
            }
        }

    }
    public void Damage(float damageAmount)
    {
        sprite.color = Color.red;
        colorIsRed = true;
        colorHitTimer = 0.1f;
        CurrentHealth -= damageAmount;
        if(CurrentHealth <= 0f){
            if(phase == 1){
                phase = 2;
                NextPhase();
            }else{
                this.enabled = false;
            }
        }
    }
    public void Activate(){
        activated = true;
        attackCooldown = 0.6f;
    }

    private void NextPhase(){

    }

    private void laserTarget(int index){
        laserArray[index].transform.position = transform.position;
        laserArray[index].transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg*Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x));
        laserArray[index].GetComponent<BossLaserScript>().duration = 0.9f;
        laserArray[index].GetComponent<BossLaserScript>().spin = false;
        laserArray[index].SetActive(true);
    }

}
