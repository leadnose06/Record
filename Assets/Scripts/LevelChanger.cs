using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LevelChanger : MonoBehaviour
{
    [SerializeField]
    private LevelConnection _connection;

    [SerializeField]
    private String targetSceneName;

    [SerializeField]
    private Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        if(_connection == LevelConnection.ActiveConnection){
            GameObject.FindWithTag("Player").transform.position = spawnPoint.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.collider.tag == "Player"){
            LevelConnection.ActiveConnection = _connection;
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
