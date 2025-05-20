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

    void Start()
    {
        phase = 1;
        activated = true;
        laserArray = new GameObject[15];
        bulletArray = new GameObject[50];
        for (int i = 0; i < 15; i++)
        {
            laserArray[i] = Instantiate(laserPattern);
            laserArray[i].GetComponent<BossLaserScript>().player = player;
            laserArray[i].SetActive(false);
        }
        for (int i = 0; i < 50; i++)
        {
            bulletArray[i] = Instantiate(bulletPattern);
            bulletArray[i].SetActive(false);
        }
        CurrentHealth = 15f;
    }

    void Update()
    {
        if (phase == 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(46, 24), 4*Time.deltaTime);
        }
        if (colorIsRed && colorHitTimer <= 0) { sprite.color = Color.white; }
        else { colorHitTimer -= Time.deltaTime; }
        attackCooldown -= Time.deltaTime;
        if (activated)
        {
            if (attackCooldown <= 0)
            {
                if (phase == 1)
                {
                    attackChoice = Random.Range(0, 2);
                    if (attackChoice == 0)
                    {

                        LaserTarget();

                    }
                    else
                    {
                        LaserRows(1.1f);
                    }
                }
                else
                {
                    attackChoice = Random.Range(0, 3);
                    switch (attackChoice)
                    {
                        case 0:
                            LaserTarget();
                            Invoke("LaserTarget", 0.7f);
                            Invoke("LaserTarget", 1.4f);
                            break;
                        case 1:
                            LaserRows(2f);
                            break;
                        case 2:
                            BulletCircle();
                            break;
                        default:
                            break;
                    }
                }
                attackCooldown = 4f;
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
                attackCooldown = 7f;
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
        CurrentHealth = 50;
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
        for (int i = 1; i < 7; i += 2)
        {
            laserArray[i].transform.eulerAngles = new Vector3(0, 0, 0);
            laserArray[i].transform.position = new Vector3(transform.position.x - width * i, transform.position.y - 30f, 0);
            laserArray[i].GetComponent<BossLaserScript>().duration = 0.6f;
            laserArray[i].GetComponent<BossLaserScript>().spin = false;
            laserArray[i].SetActive(true);

            laserArray[i + 1].transform.eulerAngles = new Vector3(0, 0, 0);
            laserArray[i + 1].transform.position = new Vector3(transform.position.x + width * i, transform.position.y - 30f, 0);
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

}
