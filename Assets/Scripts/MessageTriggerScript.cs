using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTriggerScript : MonoBehaviour
{

    public string message;
    public GameObject UIMessage;
    public GameObject messageTrigger;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
            UIMessage.GetComponent<MessageScript>().SetMessage(message);
            messageTrigger.SetActive(false);
        }
        
    }

}
