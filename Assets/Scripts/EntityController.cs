using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on Unity Singleton example https://wiki.unity3d.com/index.php/Singleton
public class EntityController : MonoBehaviour
{
    public float speed;
    public int health;

    protected void DisplayDamage(float length)
    {
        StartCoroutine(ShadeSprite(128f / 255f, 0));
        StartCoroutine(ShadeSprite(1, length));
    }

    protected IEnumerator ShadeSprite(float value, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(value, value, value);
    }
}
