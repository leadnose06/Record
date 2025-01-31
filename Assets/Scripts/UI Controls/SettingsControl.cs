using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsControl : MonoBehaviour
{
    public Slider VolumeSlider;
    public TMP_Text volumeDisplay;
    public int volume;
    void Awake()
    {

        //TODO: get saved volume value and set volume to it
        volume = 10;
        volumeDisplay.text = volume+"";
        gameObject.SetActive(false);
    }

    void OnEnable(){
        VolumeSlider.value = volume;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (VolumeSlider.gameObject);
        
    }

    public void volumeChange(){
        volume = (int)VolumeSlider.value;
        volumeDisplay.text = volume+"";
    }

}
