using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TorchController : MonoBehaviour
{
    private static Dictionary<string, bool> mapTorches = new Dictionary<string, bool>
    {
        { "Scene2", false },
        { "Scene5", false },
        { "Scene11", false },
        { "Scene12", false }
    };

    public static void ResetTorches()
    {
        foreach(string key in mapTorches.Keys.ToList())
        {
            mapTorches[key] = false;
        }
    }

    private Animator animator;
    private bool lit = false;

    public bool openingTorch;
    public bool mainMenuTorch;

    public void SetLit(bool lit)
    {
        this.lit = lit;
        animator.SetBool("lit", true);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // Check if we have already lit this torch or not
        string loadedScene = SceneManager.GetActiveScene().name;
        if (mainMenuTorch || (mapTorches.ContainsKey(loadedScene) && mapTorches[loadedScene]))
        {
            SetLit(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!lit && !openingTorch)
        {
            if (LayerMask.LayerToName(collision.gameObject.layer) == PlayerController.PLAYER_PROJECTILE)
            {
                SetLit(true);
                mapTorches[SceneManager.GetActiveScene().name] = true;
                PlayerController.instance.torchesLit += 1;
            }
        }
    }
}