using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    private float startTime;
    private float restSeconds;
    private float roundedRestSeconds;
    private float displaySeconds;
    private float displayMinutes;

    public int countDownSeconds;

    void Awake()
    {
        startTime = Time.time;
    }

    void OnGUI()
    {
        //make sure that your time is based on when this script was first called
        //instead of when your game started
        float guiTime = Time.time - startTime;

        restSeconds = countDownSeconds - guiTime;

        //display messages or whatever here -->do stuff based on your timer
        if (restSeconds == 60)
        {
            print("One Minute Left");
        }
        if (restSeconds == 0)
        {
            print("Time is Over");
            //do stuff here
        }

        //display the timer
        roundedRestSeconds = Mathf.CeilToInt(restSeconds);
        displaySeconds = roundedRestSeconds % 60;
        displayMinutes = roundedRestSeconds / 60;

        string text = string.Format("{0:00}:{1:00}", displayMinutes, displaySeconds);
        GUI.Label(new Rect(500, 10, 100, 30), text);
    }
}
