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
    public Sprite flyingBoss;
    private float laserTargetCooldown;
    private int laserQueue;

    void Start()
    {
        phase = 1;
        //activated = true;
        laserArray = new GameObject[20];
        bulletArray = new GameObject[100];
        for (int i = 0; i < 20; i++)
        {
            laserArray[i] = Instantiate(laserPattern);
            laserArray[i].GetComponent<BossLaserScript>().player = player;
            laserArray[i].SetActive(false);
        }
        for (int i = 0; i < 100; i++)
        {
            bulletArray[i] = Instantiate(bulletPattern);
            bulletArray[i].SetActive(false);
        }
        CurrentHealth = 50f;
        laserQueue = 0;
        laserTargetCooldown = 0;
    }

    void Update()
    {
        if (phase == 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(46, 24), 7 * Time.deltaTime);
        }
        if (colorIsRed && colorHitTimer <= 0) { sprite.color = Color.white; }
        else { colorHitTimer -= Time.deltaTime; }
        attackCooldown -= Time.deltaTime;
        if (activated)
        {
            laserTargetCooldown -= Time.deltaTime;
            if (laserQueue >= 1 && laserTargetCooldown <= 0f)
            {
                LaserTarget();
                laserQueue--;
                laserTargetCooldown = 1f;
            }
            if (attackCooldown <= 0)
            {
                if (phase == 1)
                {
                    attackChoice = Random.Range(0, 2);
                    if (attackChoice == 0)
                    {

                        laserQueue += 3;
                        attackCooldown = 2f;

                    }
                    else
                    {
                        LaserRows(1.1f);
                        attackCooldown = 2f;
                    }
                }
                else
                {
                    attackChoice = Random.Range(0, 4);
                    switch (attackChoice)
                    {
                        case 0:
                            laserQueue += 3;
                            attackCooldown = 2.5f;
                            break;
                        case 1:
                            LaserRows(2.1f);
                            attackCooldown = 2.75f;
                            break;
                        case 2:
                            BulletCircle();
                            attackCooldown = 3.2f;
                            break;
                        case 3:
                            if (Random.Range(0, 2) == 0)
                            {
                                LaserSpin(5, true);
                            }
                            else
                            {
                                LaserSpin(5, false);
                            }
                            attackCooldown = 3.5f;
                            break;
                        default:
                            break;
                    }
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
        if (CurrentHealth <= 0f)
        {
            if (phase == 1)
            {
                phase = 2;
                NextPhase();
                attackCooldown = 5f;
                laserQueue = 0;
            }
            else
            {
                this.enabled = false;
                gameObject.SetActive(false);
            }
        }
    }
    public void Activate()
    {
        activated = true;
        attackCooldown = 0.6f;
    }

    private void NextPhase()
    {
        GetComponent<SpriteRenderer>().sprite = flyingBoss;
        for (int i = 0; i < laserArray.Length; i++)
        {
            laserArray[i].SetActive(false);
        }
        CurrentHealth = 100f;
    }

    private void LaserTarget()
    {
        int index = 0;
        for (int i = 0; i < laserArray.Length; i++)
        {
            if (laserArray[i].activeSelf == false)
            {
                index = i;
                break;
            }
        }
        laserArray[index].transform.eulerAngles = new Vector3(0, 0, 0);
        laserArray[index].transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f, 0);
        laserArray[index].transform.RotateAround(transform.position + new Vector3(0, 0.75f, 0), new Vector3(0, 0, 1), Mathf.Rad2Deg * Mathf.Atan2(player.transform.position.y - (transform.position.y + 0.75f), player.transform.position.x - transform.position.x) - 90f);
        //laserArray[index].transform.RotateAround(new Vector3(transform.position.x, transform.position.y+2f, 0f), new Vector3(0,0,1), Mathf.Rad2Deg*Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x));
        laserArray[index].GetComponent<BossLaserScript>().duration = 0.6f;
        laserArray[index].GetComponent<BossLaserScript>().spin = false;
        laserArray[index].SetActive(true);
    }

    private void LaserRows(float width)
    {
        int index = 0;
        for (int i = 0; i < laserArray.Length; i++)
        {
            if (laserArray[i].activeSelf == false)
            {
                index = i;
                break;
            }
        }   
        for (int i = 1+index; i < index+7; i += 2)
        {
            laserArray[i].transform.eulerAngles = new Vector3(0, 0, 0);
            laserArray[i].transform.position = new Vector3(transform.position.x - width * (i-index), transform.position.y - 30f, 0);
            laserArray[i].GetComponent<BossLaserScript>().duration = 0.6f;
            laserArray[i].GetComponent<BossLaserScript>().spin = false;
            laserArray[i].SetActive(true);

            laserArray[i + 1].transform.eulerAngles = new Vector3(0, 0, 0);
            laserArray[i + 1].transform.position = new Vector3(transform.position.x + width * (i-index), transform.position.y - 30f, 0);
            laserArray[i + 1].GetComponent<BossLaserScript>().duration = 0.6f;
            laserArray[i + 1].GetComponent<BossLaserScript>().spin = false;
            laserArray[i + 1].SetActive(true);
        }
    }
    private void BulletCircle()
    {
        
        for (int i = 0; i < 45; i++)
        {
            float angle = i * 8f;
            bulletArray[i].GetComponent<BossBulletScripe>().direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
            bulletArray[i].transform.position = transform.position + new Vector3(0, 0.5f, 0);
            bulletArray[i].SetActive(true);
        }
    }

    private void LaserSpin(int number, bool clockwise)
    {
        int highestIndex = 14;
        for (int i = 14; i > 0; i--)
        {
            if (laserArray[i].activeSelf == false)
            {
                highestIndex = i;
                break;
            }
        }
        for (int i = highestIndex; i > highestIndex - number; i--)
        {
            laserArray[i].transform.eulerAngles = new Vector3(0, 0, 0);
            laserArray[i].GetComponent<BossLaserScript>().point = transform.position;
            laserArray[i].transform.position = transform.position + new Vector3(0, 0.75f, 0);
            laserArray[i].transform.RotateAround(transform.position + new Vector3(0, 0.75f, 0), new Vector3(0, 0, 1), (highestIndex-i) * (360/number));
            laserArray[i].GetComponent<BossLaserScript>().duration = 4f;
            laserArray[i].GetComponent<BossLaserScript>().spin = true;
            laserArray[i].GetComponent<BossLaserScript>().clockwise = clockwise;
            laserArray[i].GetComponent<BossLaserScript>().turnSpeed = 15;
            laserArray[i].SetActive(true);
        }
    }

}
