using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Animator animator;

    private bool lit = false;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!lit)
        {
            if (LayerMask.LayerToName(collision.gameObject.layer) == PlayerController.PLAYER_PROJECTILE)
            {
                lit = true;
                animator.SetBool("lit", true);
                PlayerController.instance.torchesLit += 1;
            }
        }
    }
}
