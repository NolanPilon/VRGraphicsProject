using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int dogCount = 0;
    private int yipeeCount = 0;

    public int ballCount;

    [Header("Creature UI Text")]
    [SerializeField] private TextMeshProUGUI dogCounterUI;
    [SerializeField] private TextMeshProUGUI yipeeCounterUI;

    [Header("Creature Trap Settings")]
    [SerializeField] private GameObject creatureBall;
    [SerializeField] private Transform spawnPos;

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

    public void spawnBall() 
    {
        if (ballCount < 1) 
        {
            Instantiate(creatureBall, spawnPos.position, Quaternion.identity);
            Debug.Log("spawned ball");
            ballCount++;
        }
    }

}
