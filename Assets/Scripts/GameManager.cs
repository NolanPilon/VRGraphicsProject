using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int dogCount = 0;
    private int yipeeCount = 0;

    [SerializeField] private TextMeshProUGUI dogCounterUI;
    [SerializeField] private TextMeshProUGUI yipeeCounterUI;


    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public void catchNotPokemon(CreatureType creature) 
    {
        if (creature.creatureType == "eggdog") 
        {
            dogCount++;
            dogCounterUI.text = "X" + dogCount;
        }

        if (creature.creatureType == "yipee")
        {
            yipeeCount++;
            yipeeCounterUI.text = "X" + yipeeCount;
        }

    }
}
