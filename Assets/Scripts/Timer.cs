using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public Color timerColor;
    private TextMesh time_text;

    private float restSeconds;
    private float roundedRestSeconds;

    public int countDownSeconds;

    public Board board;

    void Start()
    {
        restSeconds = countDownSeconds;
        time_text = GameObject.Find("time_text").GetComponent<TextMesh>();
    }

    public void StartTimer()
    {
        enabled = true;
        restSeconds = countDownSeconds;
    }

    public void Pause()
    {
        enabled = false;
    }

    public void Resume()
    {
        enabled = true;
    }

    void Update()
    {
        restSeconds -= (Time.deltaTime);

        if (restSeconds == 60)
        {
            print("One Minute Left");
        }
        if (restSeconds <= 0)
        {
            print("Time is Over");
            board.TimeOut();
            enabled = false;
        }

        roundedRestSeconds = Mathf.CeilToInt(restSeconds);
        time_text.text = roundedRestSeconds + "";
    }
}
