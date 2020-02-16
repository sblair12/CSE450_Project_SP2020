using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : EnemyController
{
    public float sightDistance;
    public int playerLayer;
    public int boundaryLayer;

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            Vector2 sight = playerTransform.position - transform.position;

            int combinedLayerMask = (1 << playerLayer) | (1 << boundaryLayer);
            RaycastHit2D lineOfSight = Physics2D.Raycast(transform.position, sight, sightDistance, combinedLayerMask);
            if (lineOfSight.collider != null && lineOfSight.collider.gameObject.GetComponent<PlayerController>())
            {
                Debug.DrawRay(transform.position, sight * sightDistance, Color.blue);
            }
        }
    }
}
