using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerScript : MonoBehaviour
{
    public float Heath = 100f;
    public float energy = 100f;

    public bool bIsEnemyNear;
    public float step = 20f;
    public float speed = 0.2f;

    private List<GameObject> nearEnemies;

    //private Animator animator;

    private void Awake()
    {
        // animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.bIsPlayerTurn)
        {
            //int movX = (int)Mathf.Round(Input.GetAxis("Horizontal"));
            //int movZ = (int)Mathf.Round(Input.GetAxis("Vertical"));
            //Debug.Log(movZ +" "+ movZ);

            if (Input.GetKeyDown(KeyCode.D))
            {
                //animator.SetBool("Moving", true);
                transform.DOMoveX(transform.position.x + step, speed).OnComplete(resetMovement);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                //animator.SetBool("Moving", true);
                transform.DOMoveX(transform.position.x - step, speed).OnComplete(resetMovement);
            }
            else if(Input.GetKeyDown(KeyCode.W))
            {
                //animator.SetBool("Moving", true);
                transform.DOMoveZ(transform.position.z + step, speed).OnComplete(resetMovement);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                //animator.SetBool("Moving", true);
                transform.DOMoveZ(transform.position.z - step, speed).OnComplete(resetMovement);
            }
        }
            
    }
    void resetMovement()
    {
        Debug.Log("reset");
        GameManager.Instance.bIsPlayerTurn = false;
        //animator.SetBool("Moving", false);
    }

    public void Attack(float outDamage, float outEnergy)
    {
        energy -= outEnergy;
        foreach(GameObject enemie in nearEnemies)
        {
            enemie.GetComponent<EnemyScript>().TakeDamage(outDamage);
        }
    }

    public void TakeDamage(float inDamage)
    {
        Heath -= inDamage;
        if(Heath <= 0)
        {
            Die();
        }
    }

    public void Die()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            bIsEnemyNear = true;
            nearEnemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            bIsEnemyNear = false;
            nearEnemies.Remove(other.gameObject);
        }
    }
}
