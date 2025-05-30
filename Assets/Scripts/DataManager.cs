using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static int StartingMaxHealth = 5;
    public static int StartingMaxHeals = 1;
    public static int StartingMaxEnergy = 10;
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
    public int playerMaxHeals = 1;
    public bool doubleJumpReady = true;
    public bool dashReady = true;
    public bool toBench;
    public bool invulnerable = false;
    public bool spawning;
    //public bool WallsLocked = true;
    public bool miniboss1Dead = false;
    public bool miniboss2Dead = false;
    public bool miniboss3Dead = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        toBench = true;
        spawning = true;
        lastBench = SceneManager.GetActiveScene().name;
        Instance = this;
        DontDestroyOnLoad(gameObject);
        playerMaxHealth = 5;
        playerMaxHeals = 1;
        playerHealth = playerMaxHealth;
        playerHeals = playerMaxHeals;
        if (playerData.collected == null)
        {
            playerData.collected = new List<string>();
        }
    }
    public void CreateSave1()
    {
        playerData = new PlayerSaveData();
        playerData.maxHealth = StartingMaxHealth;
        playerData.maxHeals = StartingMaxHeals;
        playerData.maxEnergy = StartingMaxEnergy;
        playerData.collected = new List<string>();
        playerData.collected.Add("test");
        saveFilePath1 = Application.persistentDataPath + "/PlayerSaveData1.json";
        saveNumber = 1;
        Debug.Log(saveFilePath1);

    }
    public void CreateSave2()
    {
        playerData = new PlayerSaveData();
        playerData.maxHealth = StartingMaxHealth;
        playerData.maxHeals = StartingMaxHeals;
        playerData.maxEnergy = StartingMaxEnergy;
        playerData.collected = new List<string>();
        playerData.collected.Add("test");
        saveFilePath2 = Application.persistentDataPath + "/PlayerSaveData2.json";
        saveNumber = 2;
        Debug.Log(saveFilePath2);
    }    
    public void CreateSave3()
    {
        playerData = new PlayerSaveData();
        playerData.maxHealth = StartingMaxHealth;
        playerData.maxHeals = StartingMaxHeals;
        playerData.maxEnergy = StartingMaxEnergy;
        playerData.dash = false;
        playerData.grapple = false;
        playerData.doubleJump = false;
        playerData.collected = new List<string>();
        playerData.collected.Add("test");
        saveFilePath3 = Application.persistentDataPath + "/PlayerSaveData3.json";
        saveNumber = 3;
        Debug.Log(saveFilePath3);
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
        toBench = true;
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
        playerMaxHealth = playerData.maxHealth;
        playerMaxHeals = playerData.maxHeals;
        playerMaxEnergy = playerData.maxEnergy;
        playerHealth = playerMaxHealth;
        playerHeals = playerMaxHeals;
        playerEnergy = Mathf.Round(playerMaxEnergy/2);
    }


}


[System.Serializable]
public class PlayerSaveData
{
    public int maxHealth;
    public int maxHeals;
    public int maxEnergy;
    public int currentMoney;
    public string lastBench;
    public List<string> collected;
    #region abilites
    public bool dash;
    public bool doubleJump;
    public bool grapple;
    #endregion
}
