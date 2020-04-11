using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boundary : MonoBehaviour
{
    public string sceneToSwitchTo;
    public bool horizontalSceneShift;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() && !string.IsNullOrEmpty(sceneToSwitchTo))
        {
            SceneManager.LoadScene(sceneToSwitchTo);

            float oldX = collision.gameObject.transform.position.x;
            float oldY = collision.gameObject.transform.position.y;

            if (horizontalSceneShift)
            {
                if (oldX > 0)
                {
                    oldX -= 0.2f;
                }
                else
                {
                    oldX += 0.2f;
                }

                collision.gameObject.transform.position = new Vector3(-oldX, oldY);
            }


            else
            {
                if (oldY > 0)
                {
                    oldY -= 0.4f;
                }
                else
                {
                    oldY += 0.4f;
                }

                collision.gameObject.transform.position = new Vector3(oldX, -oldY);
            }
        }
    }

    private void AutoSave()
    {

    }
}
