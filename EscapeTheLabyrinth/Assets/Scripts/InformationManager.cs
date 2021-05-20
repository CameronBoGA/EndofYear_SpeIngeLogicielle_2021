using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//GetComponent<TMPro.TextMeshProUGUI>().text
public class InformationManager : MonoBehaviour
{
    ////////// Overall Data /////////////////
    /* GameObject */
    public GameObject desc_wall;
    public GameObject desc_killer;
    public GameObject desc_mud;

    /* UI Inputs */
    public InputField min_in;
    public InputField max_in;

    ////////// Wall Data /////////////////
    /* UI Display */
    public TextMeshProUGUI wall_min;
    public TextMeshProUGUI wall_max;

    ////////// Player Data /////////////////
    /* UI Display */
    public TextMeshProUGUI player_min;
    public TextMeshProUGUI player_max;

    ////////// Mud Data /////////////////
    /* UI Display */
    public TextMeshProUGUI mud_min;
    public TextMeshProUGUI mud_max;

    ////////// KillerField Data /////////////////
    /* UI Display */
    public TextMeshProUGUI killer_min;
    public TextMeshProUGUI killer_max;
    


    void Start() {

        /* Initial Display Of Wall Related Text */
        wall_min.text += "Min: 0";
        wall_max.text += "Max: 100";
        
        /* Initial Display Of Wall Related Text */
        player_min.text += "Min: 1";
        player_max.text += "Max: 1";

        /* Initial Display Of Wall Related Text */
        mud_min.text += "Min: 0";
        mud_max.text += "Max: 5";

        /* Initial Display Of Wall Related Text */
        killer_min.text += "Min: 0";
        killer_max.text += "Max: 1";
    }
}
