using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool bIsPlayerTurn;

    public delegate void OnEnemiesTurn();
    public static event OnEnemiesTurn EnemiesTurn;

    //public List<EnemyScript> enemiesList = new List<EnemyScript>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;

        bIsPlayerTurn = true;
    }

    private void Update()
    {
        if (!bIsPlayerTurn)
        {
            EnemiesTurn?.Invoke();
            bIsPlayerTurn = true;

        }
    }

}