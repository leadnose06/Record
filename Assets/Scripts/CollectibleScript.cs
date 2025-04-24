using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public enum collectibles{
        Dash,
        Grapple,
        DoubleJump
    }
    public collectibles value;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            switch (value)
            {
                case collectibles.Dash:
                DataManager.Instance.playerData.dash = true;
                break;

                case collectibles.Grapple:
                DataManager.Instance.playerData.grapple = true;
                break;

                case collectibles.DoubleJump:
                DataManager.Instance.playerData.doubleJump = true;
                break;
                
                default:
                break;
            }
        }
        Destroy(gameObject);
    }
}
