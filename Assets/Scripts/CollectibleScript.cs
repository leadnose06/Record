using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public string collectableName;
    public enum collectibles
    {
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
        
        ArrayList collected = DataManager.Instance.playerData.collected;

        canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (collected.Count > 0)
        {
            if (collected.IndexOf(collectableName) >= 0)
            {
                gameObject.SetActive(false);
            }
        }

        
    }
    public string GetName() { return name; }

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
            DataManager.Instance.playerData.collected.Add(collectableName);
            Debug.Log("Collectable added");
            Destroy(gameObject);
        }
    }
}
