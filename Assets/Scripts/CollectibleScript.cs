using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public enum collectibles{
        Dash,
        Grapple,
        DoubleJump,
        Heart,
        Nano
    }
    public GameObject canvas;
    public collectibles value;
    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

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

                case collectibles.Heart:
                canvas.GetComponent<CanvasScript>().addHeart();
                break;

                case collectibles.Nano:
                canvas.GetComponent<CanvasScript>().addNano();
                break;

                default:
                break;
            }
            Destroy(gameObject);
        }
    }
}
