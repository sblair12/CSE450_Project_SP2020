using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedincrease : MonoBehaviour
{
    public string type;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            Destroy(gameObject);
            PlayerController.speed = Speedincrease * 2;
        }
    }
}