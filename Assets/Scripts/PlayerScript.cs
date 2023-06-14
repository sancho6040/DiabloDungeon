using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float Heath = 100f;
    public float energy = 100f;

    public Slider hpSlider, energySlider;
    public GameObject gameOver;

    public bool bIsEnemyNear;
    public float step = 20f;
    public float speed = 0.2f;

    private bool bCanMove = true;

    private List<GameObject> nearEnemies;

    public Animator animator;

    private Vector3[] directions =
    {
        Vector3.forward,
        Vector3.back,
        Vector3.left,
        Vector3.right
    };

    private void Awake()
    {
    }

    private void Update()
    {
        if (GameManager.Instance.bIsPlayerTurn)
        {
            List<bool> bClear = new List<bool>();

            foreach (Vector3 direction in directions)
            {
                RaycastHit hit;
                bool hitResult = Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, 5f);
                bClear.Add(hitResult);

                if (hitResult && hit.collider.CompareTag("Enemy"))
                {
                    nearEnemies.Add(hit.transform.gameObject);
                    bCanMove = false;
                }
            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 5f, bClear[0] ? Color.yellow : Color.white);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 5f, bClear[1] ? Color.yellow : Color.white);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 5f, bClear[2] ? Color.yellow : Color.white);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 5f, bClear[3] ? Color.yellow : Color.white);


            if (bCanMove)
            {
                if (Input.GetKeyDown(KeyCode.D) && !bClear[0])
                {
                    //animator.SetBool("Moving", true);
                    animator.Play("Knight_Run");
                    transform.DOMoveX(transform.position.x + step, speed).OnComplete(resetMovement);
                }
                else if (Input.GetKeyDown(KeyCode.A) && !bClear[1])
                {
                    //animator.SetBool("Moving", true);
                    animator.Play("Knight_Run");
                    transform.DOMoveX(transform.position.x - step, speed).OnComplete(resetMovement);
                }
                else if (Input.GetKeyDown(KeyCode.W) && !bClear[2])
                {
                    //animator.SetBool("Moving", true);
                    animator.Play("Knight_Run");
                    transform.DOMoveZ(transform.position.z + step, speed).OnComplete(resetMovement);
                }
                else if (Input.GetKeyDown(KeyCode.S) && !bClear[3])
                {
                    //animator.SetBool("Moving", true);
                    animator.Play("Knight_Run");
                    transform.DOMoveZ(transform.position.z - step, speed).OnComplete(resetMovement);
                }
            }

        }
        updateUI();

    }

    void updateUI()
    {
        hpSlider.value = Heath / 100f;
        energySlider.value = energy / 100f;
    }

    void SetMove()
    {
        bCanMove = true;
    }
    void resetMovement()
    {
        GameManager.Instance.bIsPlayerTurn = false;
        Invoke("SetMove", 0.2f);
        //animator.SetBool("Moving", false);
        animator.Play("Warrior_Idle");
    }

    public void Attack(float outDamage, float outEnergy)
    {
        foreach (GameObject enemie in nearEnemies)
        {
            energy -= outEnergy;
            enemie.GetComponent<EnemyScript>().TakeDamage(outDamage);
        }
    }

    public void TakeDamage(float inDamage)
    {
        Heath -= inDamage;
        if (Heath <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
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
