﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public Texture healthTexture;

    private void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 45, 10, 90, 90));

        int health = GameObject.Find("Player").GetComponent<PlayerController>().health;

        // Assume for now health is 3
        for (int i = 0; i < health; i++)
        {
            // Debug.Log(health);
            GUI.Box(new Rect(i * 30, 0, 30, 30), healthTexture);
        }

        GUI.EndGroup();
    }
}
