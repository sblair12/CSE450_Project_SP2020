using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : EntityController
{
    private static PlayerController instance;
    private static float timeOfCollision;

    // Outlets
    Rigidbody2D _rb;
    public Transform aimPivot;
    public GameObject projectilePrefab;
    public float damageRecoveryTime;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _rb.AddForce(Vector2.left * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rb.AddForce(Vector2.right * speed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            _rb.AddForce(Vector2.up * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _rb.AddForce(Vector2.down * speed);
        }

        // Aim reticle with mouse
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * 180f / Mathf.PI;

        aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

        // Shoot with left mouse
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyController>() && Time.time - timeOfCollision > damageRecoveryTime)
        {
            timeOfCollision = Time.time;
            if (health > 1)
            {
                health--;
                DisplayDamage(damageRecoveryTime);
            }
            else
            {
                // Destroy HealthStatus and Player
                Destroy(GameObject.Find("HealthStatus"));
                Destroy(this.gameObject);

                // Load the initial scene on death for now
                SceneManager.LoadScene(0);
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.GetComponent<EnemyController>())
    //    {
    //        if (health > 1)
    //        {
    //            health--;
    //            DisplayDamage(0.25f);
    //        }
    //        else
    //        {
    //            // Destroy HealthStatus and Player
    //            Destroy(GameObject.Find("HealthStatus"));
    //            Destroy(this.gameObject);

    //            // Load the initial scene on death for now
    //            SceneManager.LoadScene(0);
    //        }
    //    }
    //}
}
