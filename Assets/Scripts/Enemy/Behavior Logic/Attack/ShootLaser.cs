using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Shoot Laser", menuName = "Enemy Logic/Attack Logic/Shoot Laser")]

public class ShootLaser : EnemyAttackSOBase
{
    public GameObject laser;
    public float timer;
    public float shootFrequency = 2f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootFrequency){
            timer = 0f;
            shoot();
        }
    }
    public void shoot(){
        Vector2 laserPos = new Vector2((enemy.transform.position.x - (0.2*enemy.transform.localScale.x)), enemy.transform.position.y-0.15);
        Instantiate(laser, laserPos, Quaternion.identity);
        Debug.Log("shot laser");
    }
}
