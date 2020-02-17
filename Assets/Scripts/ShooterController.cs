using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : EnemyController
{
    public GameObject projectilePrefab;

    public float sightDistance;
    public int playerLayer;
    public int boundaryLayer;
    public float shootDelay;

    float shootTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            Vector2 sight = playerTransform.position - transform.position;

            // Combine Player and Boundary layer masks in order to determine when Player is in line of sight
            int combinedLayerMask = (1 << playerLayer) | (1 << boundaryLayer);
            RaycastHit2D lineOfSight = Physics2D.Raycast(transform.position, sight, sightDistance, combinedLayerMask);
            if (lineOfSight.collider != null && lineOfSight.collider.gameObject.GetComponent<PlayerController>() && Time.time > shootTime)
            {
                float radiansToMouse = Mathf.Atan2(sight.y, sight.x);
                float angleToMouse = radiansToMouse * 180f / Mathf.PI;
                Quaternion rotation = Quaternion.Euler(0, 0, angleToMouse);

                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.layer = LayerMask.NameToLayer("EnemyProjectile");
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = rotation;

                shootTime = Time.time + shootDelay;
            }
        }
    }
}
