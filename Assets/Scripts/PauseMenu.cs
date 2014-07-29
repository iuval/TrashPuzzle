using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public Board board;
    public GUISkin skin;

    public void Open()
    {
        GetComponent<Animator>().SetTrigger("open_menu");
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUI.skin = skin;

        //the menu background box
        GUI.Box(new Rect(0, 0, 360, 370), "");
        GUI.Label(new Rect(0, 30, 360, 60), "Top Time: 500");

        GUI.Label(new Rect(0, 60, 360, 60), "Time: 30:25");

        if (GUI.Button(new Rect(30, 125, 300, 50), "RESUME"))
        {
            enabled = false;
            board.PrepareBoard();
        }

        GUI.Label(new Rect(30, 100, 300, 50), "A.I won the game");


        //if (GUI.Button(new Rect(30, 187, 300, 50), "START PONG"))
        //{
        //    GameManager manager = FindObjectOfType<GameManager>();
        //    manager.Reset();
        //    Time.timeScale = 1f;
        //    enabled = false;
        //    mustUpdate = false;
        //}

        // on/off sound buttons
        //if (GUI.Toggle(new Rect(30, 227, 300, 50), GameManager.volume == 0, "SOUND"))
        //{
        //    if (GameManager.volume == 1)
        //    {
        //        Prefs.SetSound(0);
        //        GameManager.volume = 0;
        //    }
        //}
        //else
        //{
        //    if (GameManager.volume == 0)
        //    {
        //        Prefs.SetSound(1);
        //        GameManager.volume = 1;
        //    }
        //}

        //quit button
        if (GUI.Button(new Rect(30, 287, 300, 50), "QUIT"))
        {
            Application.Quit();
        }
    }
}
