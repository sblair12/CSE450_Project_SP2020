﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : EntityController
{
    public static String PLAYER_PROJECTILE = "PlayerProjectile";

    public static PlayerController instance;
    private static bool isPaused = false;
    private static float timeOfCollision;

    public int requiredTorches;
    public int torchesLit = 0;

    // DEBUG God Mode
    private bool godMode = false;

    // Outlets
    Rigidbody2D _rb;
    SpriteRenderer _sr;

    // Texture outlets for perspective based on Player orientation
    public Sprite horizontalPerspective, upPerspective, downPerspective;

    public Transform aimPivot;
    public GameObject projectilePrefab;
    public float damageRecoveryTime;

    public int maxShots;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (health > 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                _rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
            }
            if (Input.GetKey(KeyCode.D))
            {
                _rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
            }
            if (Input.GetKey(KeyCode.W))
            {
                _rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
            }
            if (Input.GetKey(KeyCode.S))
            {
                _rb.AddForce(Vector2.down * speed, ForceMode2D.Impulse);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            // Pause menu
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;

                PauseMenuController.instance.ToggleMenu(isPaused);
            }

            if (!isPaused)
            {
                // Aim reticle with mouse
                Vector3 mousePosition = Input.mousePosition;
                Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

                float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
                float angleToMouse = radiansToMouse * 180f / Mathf.PI;

                aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

                // Orient character based on mouse angle
                float absAngle = Mathf.Abs(angleToMouse);

                // Horizontal aim
                if (absAngle > 135 || absAngle < 35)
                {
                    _sr.sprite = horizontalPerspective;

                    // Facing to the right
                    if (absAngle < 45)
                    {
                        _sr.flipX = false;
                    }
                    // Facing to the left
                    else
                    {
                        _sr.flipX = true;
                    }
                }
                // Vertical aim
                else
                {
                    _sr.flipX = false;
                    // Facing up
                    if (angleToMouse > 0)
                    {
                        _sr.sprite = upPerspective;
                    }
                    else
                    {
                        _sr.sprite = downPerspective;
                    }
                }

                // Shoot with left mouse
                Projectile[] playerProjectilesLoaded = GameObject.FindObjectsOfType<Projectile>();
                playerProjectilesLoaded = Array.FindAll(playerProjectilesLoaded, p => p.isPlayerProjectile);
                if (Input.GetMouseButtonDown(0) && playerProjectilesLoaded.Length < maxShots)
                {
                    GameObject newProjectile = Instantiate(projectilePrefab);
                    newProjectile.layer = LayerMask.NameToLayer(PLAYER_PROJECTILE);
                    newProjectile.GetComponent<Projectile>().isPlayerProjectile = true;
                    newProjectile.transform.position = transform.position;
                    newProjectile.transform.rotation = aimPivot.rotation;
                }
            }
        }
    }

    public void ToggleGodMode()
    {
        instance.godMode = !instance.godMode;
        if (instance.godMode)
        {
            instance.health = 1000;
            instance.speed = 10;
        }
        else
        {
            instance.health = 3;
            instance.speed = 3;
        }
        
    }

    public void LoadMainMenu()
    {
        // Destroy HealthStatus and Player
        Destroy(this.gameObject);
        TorchController.ResetTorches();
        TutorialTextFadeController.alreadyFaded = false;
        SceneManager.LoadScene("MainMenuScene");
    }

    private void handleCollision(Collision2D collision)
    {
        if ((collision.gameObject.GetComponent<EnemyController>() || collision.gameObject.GetComponent<Projectile>()) && Time.time - timeOfCollision > damageRecoveryTime)
        {
            timeOfCollision = Time.time;
            if (health > 1)
            {
                health--;
                DisplayDamage(damageRecoveryTime);
            }
            else if (health == 1)
            {
                health--;
                Destroy(GameObject.Find("HealthStatus"));

                // Show death screen and then load main menu
                PauseMenuController.instance.EndGame(false);
            }
        }
        else if (collision.gameObject.GetComponent<ItemController>())
        {
            // Item Types here!
            switch (collision.gameObject.GetComponent<ItemController>().type)
            {
                case "fire":
                    maxShots++;
                    break;
                case "heart":
                    if (health < 3)
                    {
                        health++;
                    }
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        handleCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        handleCollision(collision);
    }
}
