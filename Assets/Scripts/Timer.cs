using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public GUISkin skin;
    public Color timerColor;

    private float startTime;
    private float restSeconds;
    private float roundedRestSeconds;

    public int countDownSeconds;

    public Board board;

    void Start()
    {
    }

    public void StartTimer()
    {
        enabled = true;
        startTime = Time.time;
    }

    void Update()
    {
        //make sure that your time is based on when this script was first called
        //instead of when your game started
        int guiTime = (int)(Time.time - startTime);

        restSeconds = countDownSeconds - guiTime;

        //display messages or whatever here -->do stuff based on your timer
        if (restSeconds == 60)
        {
            print("One Minute Left");
        }
        if (restSeconds <= 0)
        {
            print("Time is Over");
            board.TimeOut();
            enabled = false;
            //do stuff here
        }

        //display the timer
        roundedRestSeconds = Mathf.CeilToInt(restSeconds);
    }

    void OnGUI()
    {
        GUI.skin = skin;
        GUI.color = timerColor;

        GUI.Label(new Rect(20, 30, 100, 30), roundedRestSeconds + "");
    }
}
