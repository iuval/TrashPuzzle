using UnityEngine;
using System.Collections;

public class TouchControl : MonoBehaviour
{
    private RuntimePlatform platform = Application.platform;

    private Board board;

    private bool down = false;
    private bool longTap = false;
    private bool sendTap = false;
    private float time = 0;

    private Vector2 input;

    // Use this for initialization
    void Start()
    {
        board = GetComponent<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        if (down)
        {
            time += Time.deltaTime;
            if (time > 0.5)
            {
                longTap = true;
                sendTap = true;
                down = false;
            }
        }
        if (!sendTap)
        {
            if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.touchCount > 0)
                {
                    if (!down && Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        down = true;
                        time = 0;
                    }
                    else if (down && Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        down = false;
                        sendTap = true;
                        input = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    }
                }
            }
            else
            {
                if (!down && Input.GetMouseButtonDown(0))
                {
                    down = true;
                    time = 0;
                }
                else if (down && Input.GetMouseButtonUp(0))
                {
                    down = false;
                    sendTap = true;
                    input = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
        }

        if (sendTap)
        {
            if (longTap)
            {
                longTap = false;
                if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
                {
                    board.LongTap(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
                }
                else
                {
                    board.LongTap(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
            else
            {
                board.Tap(input);
            }
            sendTap = false;
        }
    }
}
