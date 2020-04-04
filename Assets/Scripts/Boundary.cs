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


            //else
            //{
            //    if (oldPosition.y > 0)
            //    {
            //        oldPosition.x -= 0.01f;
            //    }
            //    else
            //    {
            //        oldPosition.y += 0.01f;
            //    }

            //    collision.gameObject.transform.position = new Vector3(oldPosition.x, -oldPosition.y);
            //}
        }
    }

    private void AutoSave()
    {

    }
}
