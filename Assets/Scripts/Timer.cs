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
        time_text.GetComponent<Animator>().SetBool("pulse", false);
        enabled = true;
        restSeconds = countDownSeconds;
    }

    public void Pause()
    {
        time_text.GetComponent<Animator>().SetBool("pulse", false);
        enabled = false;
    }

    public void Resume()
    {
        enabled = true;
    }

    void Update()
    {
        restSeconds -= (Time.deltaTime);

        roundedRestSeconds = Mathf.CeilToInt(restSeconds);

        if (roundedRestSeconds == 10)
        {
            time_text.GetComponent<Animator>().SetBool("pulse", true);
        }
        if (roundedRestSeconds <= 0)
        {
            time_text.GetComponent<Animator>().SetBool("pulse", false);
            board.TimeOut();
            enabled = false;
        }

        time_text.text = roundedRestSeconds + "";
    }
}
