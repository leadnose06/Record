using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DescriptionManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject[] objects;
    private string[] names;
    private string[] descriptions;
    public TMP_Text nameDisplay;
    public TMP_Text descriptionDisplay;
     private int lastIndex;
    
    void Awake()
    {
        names = new string[objects.Length];
        descriptions = new string[objects.Length];
        #region names
        names[0] = "Health";
        names[1] = "Nanobots";
        names[2] = "Dash";
        names[3] = "Grapple";
        names[4] = "Double Jump";
        #endregion

        #region descriptions
        descriptions[0] = "The remaining strength of your armor. If it runs out, you succumb to the elements";
        descriptions[1] = "Nanobots that can be used to partially repair your armor. You only have a limited amount, use them wisely";
        descriptions[2] = "A small horizontal boost. Use it to evade enemies and cross large gaps. It has a cooldown of half a second after use";
        descriptions[3] = "Your grappling hook. It can be used to gain a great deal of height by grappling onto enemies and grapple points. It takes a second to recharge afte using it.";
        descriptions[4] = "Use your double jump to reach high ledges and jump over enemies. If used alongside your other abilities, it can let you stay airborne for a very long time.";
        #endregion
    }
    void Start()
    {
        lastIndex = 0;
        nameDisplay.text = names[0];
        descriptionDisplay.text = descriptions[0];
    }

    void Update()
    {
        if(ArrayUtility.IndexOf(objects, EventSystem.current.currentSelectedGameObject) != lastIndex){
            nameDisplay.text = names[ArrayUtility.IndexOf(objects, EventSystem.current.currentSelectedGameObject)];
            descriptionDisplay.text = descriptions[ArrayUtility.IndexOf(objects, EventSystem.current.currentSelectedGameObject)];
            lastIndex = ArrayUtility.IndexOf(objects, EventSystem.current.currentSelectedGameObject);
        }
    }
}
