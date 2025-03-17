using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public int playerHealth;
    public int playerMaxHealth;
    public bool dead = false;
    public string lastBench;
    public GameObject bench;
    public float playerMaxEnergy = 10;
    public float playerEnergy = 5;
    public PlayerSaveData playerData;
    public int saveNumber;
    string saveFilePath1;
    string saveFilePath2;
    string saveFilePath3;
    public int playerHeals;
    public int playerMaxHeals = 4;
    public bool doubleJumpReady = true;
    public bool dashReady = true;
    private void Awake(){
        if(Instance != null){
            Destroy(gameObject);
            return;
        }
        lastBench = SceneManager.GetActiveScene().name;
        Instance = this;
        DontDestroyOnLoad(gameObject);
        playerMaxHealth = 4;
        playerHealth = playerMaxHealth;
        playerHeals = playerMaxHeals;
    }
    public void CreateSave1()
    {
        playerData = new PlayerSaveData();
        saveFilePath1 = Application.persistentDataPath + "/PlayerSaveData1.json";
        saveNumber = 1;

    }
    public void CreateSave2()
    {
        playerData = new PlayerSaveData();
        saveFilePath2 = Application.persistentDataPath + "/PlayerSaveData2.json";
        saveNumber = 2;
    }    
    public void CreateSave3()
    {
        playerData = new PlayerSaveData();
        saveFilePath3 = Application.persistentDataPath + "/PlayerSaveData3.json";
        saveNumber = 3;
    }
    public void delete1(){
        saveFilePath1 = Application.persistentDataPath + "/PlayerSaveData1.json";
        if(File.Exists(saveFilePath1)){
            File.Delete(saveFilePath1);
            Debug.Log("done");
        }
    }
    public void delete2(){
        saveFilePath2 = Application.persistentDataPath + "/PlayerSaveData2.json";
        if(File.Exists(saveFilePath2)){
            File.Delete(saveFilePath2);
        }
    }
    public void delete3(){
        saveFilePath3 = Application.persistentDataPath + "/PlayerSaveData3.json";
        if(File.Exists(saveFilePath3)){
            File.Delete(saveFilePath3);
        }
    }
    public void SaveGame(){
        string savePlayerData = JsonUtility.ToJson(playerData);
        if(saveNumber == 1){
            File.WriteAllText(saveFilePath1, savePlayerData);
        }else if(saveNumber == 2){
            File.WriteAllText(saveFilePath2, savePlayerData);
        }else{
            File.WriteAllText(saveFilePath3, savePlayerData);
        }
        
    }
    public void LoadGame(){
        string savePlayerData = JsonUtility.ToJson(playerData);
        if(saveNumber == 1){
            if(File.Exists(saveFilePath1)){
            string loadPlayerData = File.ReadAllText(saveFilePath1);
            playerData = JsonUtility.FromJson<PlayerSaveData>(loadPlayerData);
        }
        }else if(saveNumber == 2){
            if(File.Exists(saveFilePath2)){
            string loadPlayerData = File.ReadAllText(saveFilePath2);
            playerData = JsonUtility.FromJson<PlayerSaveData>(loadPlayerData);
        }
        }else{
            if(File.Exists(saveFilePath3)){
            string loadPlayerData = File.ReadAllText(saveFilePath3);
            playerData = JsonUtility.FromJson<PlayerSaveData>(loadPlayerData);
        }
        }
        
    }


}


[System.Serializable]
public class PlayerSaveData{
    public int maxHealth;
    public int maxHeals;
    public int maxEnergy;
    public int currentMoney;
    public string lastBench;
    #region abilites
    public bool dash;
    public bool doubleJump;
    public bool grapple;
    #endregion
}
