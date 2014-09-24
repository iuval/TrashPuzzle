using UnityEngine;
using System.Collections;

public class MapMenuTouchControl : MonoBehaviour
{
    public Camera camera;

    private RuntimePlatform platform = Application.platform;

    private Board board;

    private bool down = false;
    private bool takeAction = false;
    private bool moving = false;

    private Vector2 input;
    private Vector2 lastInput;

    private GameObject go;

    void Update()
    {

        //if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        //{
        //    if (Input.touchCount > 0)
        //    {
        //        if (!down && Input.GetTouch(0).phase == TouchPhase.Began)
        //        {
        //            down = true;
        //            takeAction = true;
        //            input = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //        }
        //        else if (down && Input.GetTouch(0).phase == TouchPhase.Ended)
        //        {
        //            down = false;
        //            takeAction = true;
        //        }
        //        else if (down && Input.GetTouch(0).phase == TouchPhase.Moved)
        //        {
        //            moving = true;
        //            input = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //        }
        //    }
        //}
        //else
        {
            if (!down && Input.GetMouseButtonDown(0))
            {
                down = true;
                input = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                lastInput = input;
            }
            else if (down && Input.GetMouseButtonUp(0))
            {
                down = false;
                input = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                takeAction = true;
            }
            else if (down)
            {
                moving = true;
                input = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }


        if (takeAction)
        {
            takeAction = false;
            if (!down)
            {
                RaycastHit2D hit = Physics2D.Raycast(input, Vector2.zero);
                if (hit != null && hit.collider != null && hit.collider.tag == "LevelButton")
                {
                    int level = hit.transform.gameObject.GetComponent<LevelButton>().level;
                    Board.CurrentLevel = level;
                    Application.LoadLevel("game");
                }
                else
                {
                    moving = true;
                }
            }
        }


        if (moving)
        {
            camera.transform.position += (Vector3)(lastInput - input) * 0.8f;

            lastInput = input;

            moving = false;
        }
    }
}
