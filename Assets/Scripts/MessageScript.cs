using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageScript : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI text;
    public bool blank;
    private float timer;
    public float timerLength = 5f;
    void Start()
    {
        text.SetText(" ");
        blank = true;
        timer = timerLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (!blank && timer <= 0f)
        {
            blank = true;
            timer = timerLength;
            text.SetText(" ");
        }
        else if (!blank)
        {
            timer -= Time.deltaTime;
        }
    }
    public void SetMessage(string message)
    {
        text.SetText(message);
        blank = false;
        timer = 5f;
    }
}
