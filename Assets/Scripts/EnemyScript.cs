using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class EnemyScript : MonoBehaviour
{
    public float health = 30f;
    public float damage = 10f;
    public float speed = 0.2f;
    public float step = 5f;

    private PlayerScript player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    public void move()
    {
        int direction = Random.Range(1, 5);

        switch (direction)
        {
            case 1:
                transform.DOMoveX(transform.position.x + step, speed);
                break;

            case 2:
                transform.DOMoveX(transform.position.x - step, speed);
                break;

            case 3:
                transform.DOMoveZ(transform.position.z + step, speed);
                break;

            case 4:
                transform.DOMoveZ(transform.position.z - step, speed);
                break;
        }
    }

    public void Attack()
    {
        if(player != null)
        {
            player.TakeDamage(damage);
        }
    }

    public void TakeDamage(float inDamage)
    {
        health -= inDamage;
    }

    public void Die()
    {

    }
}
