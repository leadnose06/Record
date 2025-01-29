using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = DataManager.Instance.playerMaxHealth;
        checkMax();
        health = DataManager.Instance.playerHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if(health > maxHealth){
            health = maxHealth;
        }
        for(int i = 0; i < hearts.Length; i++){
            if(i < health){
                hearts[i].sprite = fullHeart;
            }else{
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void checkMax(){
        for(int i = 0; i < hearts.Length; i++){
            if(i < maxHealth){
                hearts[i].enabled = true;
            }else{
                hearts[i].enabled = false;
            }
        }
    }

    public int GetMaxHealth() {return maxHealth;}
    public int GetCurrentHealth() {return health;}

}
