using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Burst.CompilerServices;


public class EnemyScript : MonoBehaviour
{
    public float health = 30f;
    public float damage = 10f;
    public float speed = 0.2f;
    public float step = 5f;

    public bool bIsPlayerNear;

    private PlayerScript player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        GameManager.EnemiesTurn += DoYaThing;
    }

    public void DoYaThing()
    {
        if (bIsPlayerNear)
        {
            Attack();
            return;
        }

        Move();
    }

    public void Move()
    {
        if ((Random.Range(1, 3) % 1) == 0)
        {

            int direction = Random.Range(1, 5);

            switch (direction)
            {
                case 1:
                    if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 5f))
                    {
                        transform.DOMoveX(transform.position.x + step, speed);
                    }
                    break;

                case 2:
                    if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), 5f))
                    {
                        transform.DOMoveX(transform.position.x - step, speed);
                    }
                    break;

                case 3:
                    if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), 5f))
                    {
                        transform.DOMoveZ(transform.position.z + step, speed);
                    }
                    break;

                case 4:
                    if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), 5f))
                    {
                        transform.DOMoveZ(transform.position.z - step, speed);
                    }
                    break;
            }
        }
    }

    public void Attack()
    {
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }

    public void TakeDamage(float inDamage)
    {
        health -= inDamage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.EnemiesTurn -= DoYaThing;
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        bIsPlayerNear = other.CompareTag("Player");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bIsPlayerNear = false;
        }
    }
}
