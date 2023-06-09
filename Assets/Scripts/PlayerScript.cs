using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerScript : MonoBehaviour
{
    public bool bPlayerTurn;
    public float step = 20f;
    public float speed = 0.2f;

    private Animator animator;

    private void Awake()
    {
        // animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bPlayerTurn = GameManager.Instance.bIsPlayerTurn;

        if (bPlayerTurn & Input.GetKeyDown(KeyCode.RightArrow))
        {
            animator.SetBool("Moving", true);
            transform.DOMoveX(transform.position.x + step, speed).OnComplete(() =>
            {
                bPlayerTurn = false;
                animator.SetBool("Moving", false);
            });
        }
    }
}
