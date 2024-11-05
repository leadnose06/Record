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
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if(_connection == LevelConnection.ActiveConnection){
            player.transform.position = spawnPoint.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        Debug.Log("o");
        if (collision.collider.tag.Equals("Player")){
            LevelConnection.ActiveConnection = _connection;
            SceneManager.LoadScene(targetSceneName);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(targetSceneName));
        }
    }
}
