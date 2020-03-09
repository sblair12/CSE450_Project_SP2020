using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController
{
    // Outlets
    public float stoppingSpeedMultiplier;

    protected Rigidbody2D _rb;
    protected Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(playerTransform.position, transform.position) < 7)
        {
            if (transform != null && playerTransform != null)
            {
                Vector2 vectorTowardsPlayer = playerTransform.position - transform.position;

                if (Vector2.Distance(playerTransform.position, transform.position) > 0.2)
                {
                    transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
                }
                //if (Vector2.Angle(_rb.velocity, vectorTowardsPlayer) > 90)
                //{
                //    _rb.AddForce((vectorTowardsPlayer) * stoppingSpeedMultiplier * speed * Time.deltaTime);
                //}
                //else
                //{
                //    _rb.AddForce((vectorTowardsPlayer) * speed * Time.deltaTime);
                //}
            }
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Projectile>())
        {
            if (health > 1)
            {
                health--;
                DisplayDamage(0.25f);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
