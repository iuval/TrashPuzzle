﻿using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public GUISkin skin;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
        
    void OnGUI()
    {
        GUI.skin = skin;
        //layout start
        GUI.BeginGroup(new Rect(Screen.width / 2 - 180, Screen.height / 2 - 185, 360, 370));

        //the menu background box
        GUI.Box(new Rect(0, 0, 360, 370), "");
        GUI.Label(new Rect(0, 30, 360, 60), "Top Time: 500");

        GUI.Label(new Rect(0, 60, 360, 60), "Time: 30:25");

        if (GUI.Button(new Rect(30, 125, 300, 50), "RESUME"))
        {
            enabled = false;
            anim.SetTrigger("close_menu");
            anim.enabled = false;
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

        //layout end
        GUI.EndGroup();
    }
}
