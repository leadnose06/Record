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
    private string targetSceneName;

    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if(DataManager.Instance.toBench){
            //Debug.Log("tobench");
            player.transform.position = DataManager.Instance.bench.transform.position;
        } else{
            if(_connection == LevelConnection.ActiveConnection){
                //Debug.Log("todoor");
                player.transform.position = spawnPoint.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        //Debug.Log("o");
        if (other.gameObject.tag.Equals("Player")){
            DataManager.Instance.toBench = false;
            LevelConnection.ActiveConnection = _connection;
            SceneManager.LoadScene(targetSceneName);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(targetSceneName));
        }
    }
}
