using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool bIsPlayerTurn;

    public List<EnemyScript> enemiesList = new List<EnemyScript>();

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
        if(!bIsPlayerTurn)
        {
            foreach(EnemyScript enemy in enemiesList)
            {
                enemy.DoYaThing();
            }
            bIsPlayerTurn = true;
        }
    }

    public void AddEnemy(EnemyScript inEnemy)
    {
        enemiesList.Add(inEnemy);
    }

    public void RemoveEnemy(EnemyScript inEnemy)
    {
        enemiesList.Remove(inEnemy);
    }
}
